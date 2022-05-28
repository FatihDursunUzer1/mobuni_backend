using System;
using MobUni.ApplicationCore.Entities.ActivityAggregate;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
	public interface IActivityRepository:IRepository<Activity>
	{
		Task CountComment(int activityId);
		int GetActivityCountByUniversityId(int universityId, DateTime? dateTime = null);
	}
}

