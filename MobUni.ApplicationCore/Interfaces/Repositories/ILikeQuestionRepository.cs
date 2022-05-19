using MobUni.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
    public interface ILikeQuestionRepository: IRepository<LikeQuestion>
    {
        Task<bool> ChangeStatus(int questionId, string userId);
        LikeQuestion? GetByQuestionId(int questionId);
        List<LikeQuestion> GetLiked();
        List<LikeQuestion> GetLikedByUserId(string userId);
       Task<bool> LikeQuestion(int questionId, string userId);
        LikeQuestion? GetUserLikedQuestion(int questionId, string userId);
    }
}
