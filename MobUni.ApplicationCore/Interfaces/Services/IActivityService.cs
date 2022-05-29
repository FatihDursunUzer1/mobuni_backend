using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Filters;
using MobUni.ApplicationCore.Pagination;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IActivityService
	{
		 Task<bool> Delete(ActivityDTO dto);
		 Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto,string? userId=null);
		 Task<IDataResult<ActivityDTO>> Update(int activityId, int? newMaxUser, bool? timeOut);
		 Task<IDataResult<List<ActivityDTO>>> GetAll(ActivityFilter filter = null);
		 IDataResult<PaginatedList<ActivityDTO>> GetAllPaginated(ActivityFilter filter, PaginationQuery query);
		IDataResult<int> GetActivitiesByUniversityId(int universityId, DateTime? dateTime = null);
		 IDataResult<List<ActivityDTO>> GetNoTimeOuts();
		IDataResult<PaginatedList<ActivityDTO>> GetMyJoinedActivities(string userId, PaginationQuery paginationQuery);
	}
}

