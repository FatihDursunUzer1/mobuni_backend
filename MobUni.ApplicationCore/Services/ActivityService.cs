using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;

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

        public async Task<ActivityDTO> Add(CreateActivityDTO dto)
        {
            var activity = _mapper.Map<CreateActivityDTO, Activity>(dto);
            return _mapper.Map<Activity, ActivityDTO>(await _activityRepository.Add(activity));
        }

        public async Task<bool> Delete(ActivityDTO dto)
        {
            var activity = _mapper.Map<ActivityDTO, Activity>(dto);
            return await _activityRepository.Delete(activity);
        }

        public List<ActivityDTO> GetAll()
        {
            var a = _activityRepository.GetAll();
            List<ActivityDTO> activities = _mapper.Map<List<Activity>, List<ActivityDTO>>(_activityRepository.GetAll());
            return activities;
        }

        public Task<ActivityDTO> Update(ActivityDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

