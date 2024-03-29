﻿using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Expressions;
using MobUni.ApplicationCore.Filters;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Pagination;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;
using MobUni.ApplicationCore.Services.Filters;

namespace MobUni.ApplicationCore.Services
{
	public class ActivityService:IActivityService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorage _storage;
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IMapper _mapper;
		public ActivityService(IActivityRepository activityRepository,IMapper mapper,IStorage storage,IUnitOfWork unitOfWork,IHttpContextAccessor contextAccessor)
		{
            _mapper = mapper;
            _storage= storage;
            _unitOfWork= unitOfWork;
            _contextAccessor= contextAccessor;
		}

        public async Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto,string? userId=null)
        {
            var activity = _mapper.Map<CreateActivityDTO, Activity>(dto);
            if(userId is not null)
                activity.UserId = userId;
            await _unitOfWork.Activities.Add(activity, activity => activity.University, activity => activity.User);
            if (dto.Image != null)
            {
                var path = await _storage.UploadActivityImage(dto.Image, activity.Id);
                activity.Image = path;
                await _unitOfWork.Activities.Update(activity, activity.Id);
            }
            await _unitOfWork.Save();
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<Activity, ActivityDTO>(activity));
        }

        public async Task<bool> Delete(ActivityDTO dto)
        {
            var activity = _mapper.Map<ActivityDTO, Activity>(dto);
           await _unitOfWork.Activities.Delete(activity);
            await _unitOfWork.Save();
            return true;
        }

        public async Task<IDataResult<List<ActivityDTO>>> GetAll(ActivityFilter filter)
        {
            //Dışarıdan katılımcı alma durumunun kontrolü gerekiyor.
            List<ActivityDTO> activitiyDTOS =GetAllActivities(filter);
            return new SuccessDataResult<List<ActivityDTO>>(activitiyDTOS);
        }

        public IDataResult<PaginatedList<ActivityDTO>> GetAllPaginated(ActivityFilter filter,PaginationQuery query)
        {
            var paginatedList =PaginatedList<ActivityDTO>.CreateAsync( GetAllActivities(filter),query.PageIndex,query.PageSize);
            return new SuccessDataResult<PaginatedList<ActivityDTO>>(paginatedList);
        }

        private List<ActivityDTO> GetAllActivities(ActivityFilter filter,PaginationQuery? paginationQuery=null)
        {
            var activitiesFilter = new ActivitiesGetByFilter(filter);
            var activities = _unitOfWork.Activities.GetAll(filter != null ? activitiesFilter.SpecExpression : null);
            HashSet<Activity> activitySet = new HashSet<Activity>();
            HashSet<Activity> activitySetLoop = new HashSet<Activity>();
            if (filter.Categories != null) //Kategoriye göre filtreleme.
            {
                foreach (var category in filter.Categories)
                {
                    activitySetLoop = activities.Where(b => b.ActivityCategories.Contains(category)).ToHashSet<Activity>();
                    activitySet.UnionWith(activitySetLoop);
                }
                activities = activitySet.ToList();
            }
            List<ActivityDTO> activitiyDTOS = _mapper.Map<List<Activity>, List<ActivityDTO>>(activities);
            if (filter.UserId != null)
                 IsJoined(activitiyDTOS, filter.UserId);
            else
            {
                var userId = _contextAccessor.HttpContext.Items["UserId"].ToString();
                 IsJoined(activitiyDTOS, userId);
            }

            return activitiyDTOS;
        }

        private void IsJoined(List<ActivityDTO> activityDTOS,string userId)
        {
            var joinedActivityIds = _unitOfWork.ActivityParticipants.GetJoinedActivitiesIds(userId);
            foreach (var activityDto in activityDTOS)
            {
                foreach (var activityId in joinedActivityIds)
                    if (activityDto.Id == activityId)
                    {
                        activityDto.IsJoined = true;
                    }
            }

        }

        public  IDataResult<List<ActivityDTO>> GetNoTimeOuts()
        {
            return new SuccessDataResult<List<ActivityDTO>>(_mapper.Map<List<Activity>, List<ActivityDTO>>( _unitOfWork.Activities.GetAll(activity => activity.Timeout == false)));
        }

        public IDataResult<PaginatedList<ActivityDTO>> GetMyJoinedActivities(string userId, PaginationQuery paginationQuery)
        {
            var activityParticipant = _unitOfWork.ActivityParticipants.GetAll(activityParticipant => activityParticipant.UserId == userId && activityParticipant.IsActive && activityParticipant.IsJoined);
            var activities = activityParticipant.Select(activityParticipant => activityParticipant.Activity).ToList();
            var activityDtos = _mapper.Map<List<ActivityDTO>>(activities);
            activityDtos.ForEach(activityDTO => activityDTO.IsJoined = true);
            return new SuccessDataResult<PaginatedList<ActivityDTO>>(PaginatedList<ActivityDTO>.CreateAsync(activityDtos, paginationQuery.PageIndex, paginationQuery.PageSize));
        }

        public async Task<IDataResult<ActivityDTO>> Update(int activityId, int? newMaxUser, bool? timeOut)
        {
            var dbActivity=_unitOfWork.Activities.GetById(activityId);
            if ((newMaxUser!=0 || newMaxUser!=null ) && newMaxUser < dbActivity.MaxUser)
                return new ErrorDataResult<ActivityDTO>("Yeni girilen maksimum kişi sayısı, şu anki maksimum kullanıcı sayısı değerinden az olamaz",400);
            if(newMaxUser!=null)
            dbActivity.MaxUser = (int)newMaxUser;
            else if(timeOut!=null)
            dbActivity.Timeout = (bool)timeOut;
            await _unitOfWork.Activities.Update(dbActivity, activityId);
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<ActivityDTO>(dbActivity));
        }
        public IDataResult<int> GetActivitiesByUniversityId(ActivityFilter filter,DateTime? dateTime=null)
        {
            var activitiesFilter = new ActivitiesGetByFilter(filter);
            var newFilter = activitiesFilter.SpecExpression.And(x => x.CreatedTime > dateTime);
            
            var activitiesCount = _unitOfWork.Activities.GetAll(newFilter).Count();
            return new SuccessDataResult<int>(activitiesCount);
        }
    }
}

