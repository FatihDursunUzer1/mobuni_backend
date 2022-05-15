using System;
using System.Linq.Expressions;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IActivityService
	{
		public Task<bool> Delete(ActivityDTO dto);
		public Task<IDataResult<ActivityDTO>> Add(CreateActivityDTO dto);
		public Task<IDataResult<ActivityDTO>> Update(ActivityDTO dto);
		public Task<IDataResult<List<ActivityDTO>>> GetAll();
		public Task<IDataResult<List<ActivityDTO>>> GetActivitiesByUniversityId(int universityId);
		public IDataResult<ActivityDTO> GetById(int id);
	}
}

