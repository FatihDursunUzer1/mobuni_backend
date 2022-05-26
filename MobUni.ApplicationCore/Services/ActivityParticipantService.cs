using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Services;
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

        public ActivityParticipantService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result.Abstract.IDataResult<ActivityParticipantDTO>> Add(CreateActivityParticipantDTO dto, string? userId = null)
        {
            var activityParticipant = _mapper.Map<CreateActivityParticipantDTO, ActivityParticipant>(dto);
            activityParticipant.UserId= userId;
            var joinOrLeave = await _unitOfWork.ActivityParticipants.JoinOrLeave(activityParticipant);
            activityParticipant = joinOrLeave.Item1;
            //var activity= _unitOfWork.Activities.GetById(activityParticipant.ActivityId);
            if (joinOrLeave.Item2)
                activityParticipant.Activity.JoinedCount++;
            else
                activityParticipant.Activity.JoinedCount--;
            await _unitOfWork.Activities.Update(activityParticipant.Activity,activityParticipant.ActivityId);
            await _unitOfWork.Save();
            return new SuccessDataResult<ActivityParticipantDTO>(_mapper.Map<ActivityParticipant, ActivityParticipantDTO>(activityParticipant));
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
    }
}
