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
    public class LikeQuestionRepository : EfRepositoryBase<LikeQuestion>, ILikeQuestionRepository
    {
        public LikeQuestionRepository(MobUniDbContext mobUniDbContext) : base(mobUniDbContext)
        {
        }

        public async Task<bool> ChangeStatus(int questionId, string userId)
        {
           var questionLike= _mobUniDbContext.LikeQuestion.Where(question => question.QuestionId == questionId && question.UserId == userId).FirstOrDefault();
            if (questionLike != null)
            {
                questionLike.IsActive = !questionLike.IsActive;
                await Update(questionLike);
                return true;
            }
            else
            {
                var question = new LikeQuestion { QuestionId = questionId, UserId = userId, IsActive = true };
                question.CreateObject();
                await Add(question);
                return true;
            }
        }

        public async Task<bool> LikeQuestion(int questionId,string userId)
        {
            var question = new LikeQuestion { QuestionId = questionId, UserId = userId, IsActive = true };
            question.CreateObject();
            await Add(question);
            return true;
        }

        public LikeQuestion? GetByQuestionId(int questionId)
        {
            return _mobUniDbContext.LikeQuestion.Where(question=>question.QuestionId == questionId && question.IsActive).FirstOrDefault();
        }

        public List<LikeQuestion> GetLiked()
        {
            return _mobUniDbContext.LikeQuestion.Where(question => question.IsActive).ToList();
        }

        public List<LikeQuestion> GetLikedByUserId(string userId)
        {
            return _mobUniDbContext.LikeQuestion.Where(question => question.UserId == userId && question.IsActive).ToList();
        }
    }
}
