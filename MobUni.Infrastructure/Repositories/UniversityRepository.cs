using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Repositories
{
    public class UniversityRepository : EfRepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }
    }
}
