using System;
using AutoMapper;
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

        private readonly IMapper _mapper;
		public ActivityService(IActivityRepository activityRepository,IMapper mapper)
		{
            _activityRepository = activityRepository;
            _mapper = mapper;
		}

        public async Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto)
        {
            var activity = _mapper.Map<CreateActivityDTO, Activity>(dto);
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<Activity, ActivityDTO>(await _activityRepository.Add(activity)));
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
            await _activityRepository.Update(activity, activity.Id);
            return new SuccessDataResult<ActivityDTO>(_mapper.Map<ActivityDTO>(activity));
        }
    }
}

