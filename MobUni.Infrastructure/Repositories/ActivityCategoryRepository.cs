using MobUni.ApplicationCore.Entities.ActivityAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Repositories
{
    public class ActivityCategoryRepository : EfRepositoryBase<ActivityCategory>, IActivityCategoryRepository
    {
        public ActivityCategoryRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }
    }
}
