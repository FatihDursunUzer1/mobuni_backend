using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Services
{
    public class ActivityCategoryService : IActivityCategoryService
    {
        private IActivityCategoryRepository _activityCategoryRepository;
        private IMapper _mapper;

        public ActivityCategoryService(IActivityCategoryRepository activityCategoryRepository, IMapper mapper)
        {
            _activityCategoryRepository = activityCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<ActivityCategoryDTO>> Add(ActivityCategoryDTO activityCategoryDTO)
        {
            var activityCategory=_mapper.Map<ActivityCategory>(activityCategoryDTO);
            var addedActivityCategory = await _activityCategoryRepository.Add(activityCategory);
            return new SuccessDataResult<ActivityCategoryDTO>(_mapper.Map<ActivityCategoryDTO>(addedActivityCategory));

        }

        public async Task<IDataResult<List<ActivityCategoryDTO>>> GetAll()
        {
            return new SuccessDataResult<List<ActivityCategoryDTO>>(_mapper.Map<List<ActivityCategoryDTO>>(await _activityCategoryRepository.GetAll()));
        }
    }
}
