using MobUni.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
   public interface IDepartmentRepository:IRepository<Department>
    {
       Department? GetById(int id);
    }
}
