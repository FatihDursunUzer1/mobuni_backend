﻿using System;
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
		public Task<IDataResult<ActivityDTO>> Update(int activityId, int? newMaxUser, bool? timeOut);
		public Task<IDataResult<List<ActivityDTO>>> GetAll(int? pageSize, int? pageIndex, ActivityFilter filter);

		public IDataResult<List<ActivityDTO>> GetNoTimeOuts();
	}
}

