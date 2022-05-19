using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Services
{
    public interface ILikeService:IService<LikeDTO,CreateLikeDTO>
    {
        Task<IDataResult<bool>> Like(CreateLikeDTO dto, string? userId = null);
    }
}
