using System;
using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class ActivityRepository: EfRepositoryBase<Activity>, IActivityRepository
	{
        public ActivityRepository(MobUniDbContext mobUniDbContext):base(mobUniDbContext)
        {
           
        }
        public void CountComment(int activityId)
        {
            var activity = GetById(activityId);
            if (activity != null)
                activity.CommentCount++;
            _mobUniDbContext.Activities.Update(activity);
        }

        public int GetActivityCountByUniversityId(int universityId, DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                return _mobUniDbContext.Activities.Count(x => x.UniversityId == universityId);
            }
            else
            {
                return _mobUniDbContext.Activities.Count(x => x.UniversityId == universityId && x.CreatedTime > dateTime);
            }
        }
    }
}

