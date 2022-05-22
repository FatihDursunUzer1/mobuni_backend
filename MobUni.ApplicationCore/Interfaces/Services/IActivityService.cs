using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Filters;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IActivityService
	{
		public Task<bool> Delete(ActivityDTO dto);
		public Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto,string? userId=null);
		public Task<IDataResult<ActivityDTO>> Update(ActivityDTO dto);
		public Task<IDataResult<List<ActivityDTO>>> GetAll(ActivityFilter filter = null);
		public Task<IDataResult<List<ActivityDTO>>> GetActivitiesByUniversityId(int universityId);
		public IDataResult<ActivityDTO> GetById(int id);

		public Task<IDataResult<List<ActivityDTO>>> GetMyActivities(string userId);
	}
}

