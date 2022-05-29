using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Pagination;
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Services
{
    public interface IActivityParticipantService:IService<ActivityParticipantDTO,CreateActivityParticipantDTO>
    {
        Task<IDataResult<ActivityDTO>> AddParticipant(CreateActivityParticipantDTO dto, string? userId = null);
        IDataResult<PaginatedList<ActivityParticipantDTO>> GetActivityParticipantsByActivityId(int activityId, PaginationQuery paginationQuery);
    }
}
