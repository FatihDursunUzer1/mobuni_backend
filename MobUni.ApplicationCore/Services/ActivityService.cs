using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Filters;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;
using MobUni.ApplicationCore.Services.Filters;

namespace MobUni.ApplicationCore.Services
{
	public class ActivityService:IActivityService
	{
        private readonly IActivityRepository _activityRepository;
        private readonly IStorage _storage;

        private readonly IMapper _mapper;
		public ActivityService(IActivityRepository activityRepository,IMapper mapper,IStorage storage)
		{
            _activityRepository = activityRepository;
            _mapper = mapper;
            _storage= storage;
		}

        public async Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto,string? userId=null)
        {
            var activity = _mapper.Map<CreateActivityDTO, Activity>(dto);
            if(userId is not null)
                activity.UserId = userId;
            await _activityRepository.Add(activity, activity => activity.University, activity => activity.User);
            if (dto.Image != null)
            {
                var path = await _storage.UploadActivityImage(dto.Image, activity.Id);
                activity.Image = path;
                await _activityRepository.Update(activity, activity.Id);
            }
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<Activity, ActivityDTO>(activity));
        }

        public async Task<bool> Delete(ActivityDTO dto)
        {
            var activity = _mapper.Map<ActivityDTO, Activity>(dto);
            return await _activityRepository.Delete(activity);
        }

        public async Task<IDataResult<List<ActivityDTO>>> GetAll(ActivityFilter filter)
        {
            //Dışarıdan katılımcı alma durumunun kontrolü gerekiyor.
            var activitiesFilter =new ActivitiesGetByFilter(filter);
            var activities =await  _activityRepository.GetAll(filter!=null?activitiesFilter.SpecExpression:null);
            HashSet<Activity> activitySet = new HashSet<Activity>();
            HashSet < Activity > activitySetLoop=new HashSet<Activity>();
            if (filter.Categories!=null) //Kategoriye göre filtreleme.
            {
                foreach (var category in filter.Categories)
                {
                    activitySetLoop = activities.Where(b => b.ActivityCategories.Contains(category)).ToHashSet<Activity>();
                    activitySet.UnionWith(activitySetLoop);
                }
            }
            List<ActivityDTO> activitiyDTOS = _mapper.Map<List<Activity>, List<ActivityDTO>>(activitySet.ToList());
            return new SuccessDataResult<List<ActivityDTO>>(activitiyDTOS);
        }

        public async Task<IDataResult<ActivityDTO>> Update(int activityId, int? newMaxUser, bool? timeOut)
        {
            var dbActivity=_activityRepository.GetById(activityId);
            if ((newMaxUser!=0 || newMaxUser!=null ) && newMaxUser < dbActivity.MaxUser)
                return new ErrorDataResult<ActivityDTO>("Yeni girilen maksimum kişi sayısı, şu anki maksimum kullanıcı sayısı değerinden az olamaz",400);
            if(newMaxUser!=null)
            dbActivity.MaxUser = (int)newMaxUser;
            else if(timeOut!=null)
            dbActivity.Timeout = (bool)timeOut;
            await _activityRepository.Update(dbActivity, activityId);
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<ActivityDTO>(dbActivity));
        }
    }
}

