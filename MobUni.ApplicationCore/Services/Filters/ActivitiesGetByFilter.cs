using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Services.Filters
{
    public class ActivitiesGetByFilter
    {
        private readonly ActivityFilter _activityFilter;

        public ActivitiesGetByFilter(ActivityFilter activityFilter)
        {
            _activityFilter = activityFilter;
        }
        public Expression<Func<Activity, bool>> SpecExpression
        {
            get
            {
                return activity => (_activityFilter.Id == null || activity.Id == _activityFilter.Id) && (_activityFilter.UniversityId == null || activity.UniversityId == _activityFilter.UniversityId)
                && (_activityFilter.UserId == null || activity.UserId == _activityFilter.UserId) && (_activityFilter.IsExternal == null || activity.IsExternal == _activityFilter.IsExternal);
            }
        }
    }
}
