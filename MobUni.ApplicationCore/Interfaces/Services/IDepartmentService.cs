using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Services
{
    public interface IDepartmentService: IService<DepartmentDTO, CreateDepartmentDTO>
    {
    }
}
