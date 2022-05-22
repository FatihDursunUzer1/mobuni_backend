using MobUni.ApplicationCore.Entities.ActivityAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Filters
{
    public class ActivityFilter
    {
        public int Id { get; set; }

        public int UniversityId { get; set; }


        public  Expression<Func<Activity, bool>> SpecExpression
        {
            get
            {
                return activity => (Id == null || activity.Id == Id) && (UniversityId == null || activity.UniversityId == UniversityId);
            }
        }
    }
}
