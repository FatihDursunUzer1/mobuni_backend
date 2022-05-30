﻿using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Pagination;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Services
{
    public class ActivityParticipantService : IActivityParticipantService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotification _pushNotification;

        public ActivityParticipantService(IMapper mapper, IUnitOfWork unitOfWork, IPushNotification pushNotification)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pushNotification = pushNotification;
        }

        public async Task<Result.Abstract.IDataResult<ActivityDTO>> AddParticipant(CreateActivityParticipantDTO dto, string? userId = null)
        {
            var activityParticipant = _mapper.Map<CreateActivityParticipantDTO, ActivityParticipant>(dto);
            activityParticipant.UserId= userId;
            var joinOrLeave = await _unitOfWork.ActivityParticipants.JoinOrLeave(activityParticipant);
            activityParticipant = joinOrLeave.Item1;
            //var activity= _unitOfWork.Activities.GetById(activityParticipant.ActivityId);
            var isJoined = false;
            if (joinOrLeave.Item2)
            {
                activityParticipant.Activity.JoinedCount++;
                isJoined = true;
                await _pushNotification.SendActivityJoinedNotification(userId,activityParticipant.Activity.UserId,activityParticipant.ActivityId,activityParticipant.User.FullName,activityParticipant.Activity.Title);
            }
            else
            {
                activityParticipant.Activity.JoinedCount--;
            }
            await _unitOfWork.Activities.Update(activityParticipant.Activity,activityParticipant.ActivityId);
            await _unitOfWork.Save();
            var activityDTO = _mapper.Map<Activity, ActivityDTO>(activityParticipant.Activity);
            activityDTO.IsJoined = isJoined;
            return new SuccessDataResult<ActivityDTO>(activityDTO);
        }

        public IDataResult<PaginatedList<ActivityParticipantDTO>> GetActivityParticipantsByActivityId(int activityId, PaginationQuery paginationQuery)
        {
            var activityParticipant = _unitOfWork.ActivityParticipants.GetAll(activityParticipant => activityParticipant.ActivityId == activityId && activityParticipant.IsActive && activityParticipant.IsJoined);
            return new SuccessDataResult<PaginatedList<ActivityParticipantDTO>>(PaginatedList<ActivityParticipantDTO>.CreateAsync(_mapper.Map<List<ActivityParticipantDTO>>(activityParticipant), paginationQuery.PageIndex, paginationQuery.PageSize));
        }


        public Task<bool> Delete(ActivityParticipantDTO dto)
        {
            throw new NotImplementedException();
        }

        public Result.Abstract.IDataResult<List<ActivityParticipantDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result.Abstract.IDataResult<ActivityParticipantDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result.Abstract.IDataResult<ActivityParticipantDTO>> Update(ActivityParticipantDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<IDataResult<ActivityParticipantDTO>> IService<ActivityParticipantDTO, CreateActivityParticipantDTO>.Add(CreateActivityParticipantDTO dto, string? userId)
        {
            throw new NotImplementedException();
        }
    }
}
