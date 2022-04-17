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
    public class DepartmentRepository : EfRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }

        public Department? GetById(int id)
        {
            return _mobUniDbContext.Set<Department>().FirstOrDefault(department => department.Id == id);
        }
    }
}
