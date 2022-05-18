using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;

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
                await _activityRepository.Update(activity);
            }
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<Activity, ActivityDTO>(activity));
        }

        public async Task<bool> Delete(ActivityDTO dto)
        {
            var activity = _mapper.Map<ActivityDTO, Activity>(dto);
            return await _activityRepository.Delete(activity);
        }

        public async Task<IDataResult<List<ActivityDTO>>> GetAll()
        {
            var a =await  _activityRepository.GetAll();
            List<ActivityDTO> activities = _mapper.Map<List<Activity>, List<ActivityDTO>>(a);
            return new SuccessDataResult<List<ActivityDTO>>(activities);
        }
        public async Task<IDataResult<List<ActivityDTO>>> GetActivitiesByUniversityId(int universityId)
        {
            var activities = await _activityRepository.GetAll(x=>x.UniversityId==universityId);
            List<ActivityDTO> activityDtos = _mapper.Map<List<Activity>, List<ActivityDTO>>(activities);
            return new SuccessDataResult<List<ActivityDTO>>(activityDtos);
        }

        public IDataResult<ActivityDTO> GetById(int id)
        {
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<ActivityDTO>(_activityRepository.GetById(id)));
        }

        public async Task<IDataResult<ActivityDTO>> Update(ActivityDTO dto)
        {
            var activity = _mapper.Map<Activity>(dto);
            var dbActivity=_activityRepository.GetById(activity.Id);
            activity.CreatedTime = dbActivity.CreatedTime;
            activity.UpdatedTime = DateTime.Now;
            await _activityRepository.Update(activity);
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<ActivityDTO>(activity));
        }

        public async Task<IDataResult<List<ActivityDTO>>> GetMyActivities(string userId)
        {
            return new SuccessDataResult<List<ActivityDTO>>(_mapper.Map<List<ActivityDTO>>(await _activityRepository.GetAll(activity=>activity.UserId==userId)));
        }
    }
}

