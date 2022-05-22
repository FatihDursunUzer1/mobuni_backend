using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Services
{
    public interface IActivityCategoryService
    {
        Task<IDataResult<List<ActivityCategoryDTO>>> GetAll();
        Task<IDataResult<ActivityCategoryDTO>> Add(ActivityCategoryDTO activityCategoryDTO);
    }
}
