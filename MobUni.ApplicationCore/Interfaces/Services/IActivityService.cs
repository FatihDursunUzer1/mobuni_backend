using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IActivityService
	{
		public Task<bool> Delete(ActivityDTO dto);
		public Task<ActivityDTO> Add(CreateActivityDTO dto);
		public Task<ActivityDTO> Update(ActivityDTO dto);
		public Task<List<ActivityDTO>> GetAll();
	}
}

