using Microsoft.EntityFrameworkCore;
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
        private IActivityRepository _activityRepository;
        private IQuestionCommentRepository _questionCommentRepository;
        private IQuestionRepository _questionRepository;
        public LikeQuestionRepository(MobUniDbContext mobUniDbContext,IActivityRepository activityRepository, IQuestionCommentRepository questionCommentRepository, IQuestionRepository questionRepository):base(mobUniDbContext)
        {
            _activityRepository = activityRepository;
            _questionCommentRepository = questionCommentRepository;
            _questionRepository = questionRepository;
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

        public async Task<bool> LikeOrDislike(int tableType,int Id, string userId)
        {
            LikeQuestion? questionLike =new LikeQuestion();
            if(tableType==1)
                questionLike= _mobUniDbContext.LikeQuestion.Where(question => question.QuestionId == Id && question.UserId == userId ).FirstOrDefault();
            else if(tableType==2)
                questionLike= _mobUniDbContext.LikeQuestion.Where(question => question.QuestionCommentId == Id && question.UserId == userId).FirstOrDefault();
            else if(tableType==3)
                { }
            if (questionLike != null)
            {
                questionLike.IsActive = !questionLike.IsActive;
                _mobUniDbContext.LikeQuestion.Update(questionLike);
                if(tableType == 1)
                return await LikeCountQuestion(Id, questionLike.IsActive);
                else 
                    return await LikeCountComment(Id,questionLike.IsActive);
            }
            else
            {
                var question = new LikeQuestion { UserId = userId, IsActive = true };
                if (tableType == 1)
                    question.QuestionId = Id;
                else if (tableType == 2)
                    question.QuestionCommentId = Id;
                else if(tableType == 3)
                { }
                question.CreateObject();
                await _mobUniDbContext.LikeQuestion.AddAsync(question);
                if(tableType==1)
                return await LikeCountQuestion(Id, true);
                else if(tableType==2)
                    return await LikeCountComment(Id, true);
            }
            return false;
        }

        private async Task<bool> LikeCountQuestion(int Id, bool isActive)
        {
            var question = _questionRepository.GetById(Id);
            if (isActive)
                question.LikeCount++;
            else
                question.LikeCount--;
            await _questionRepository.Update(question);
            return true;
        }

        private async Task<bool> LikeCountComment(int Id, bool isActive)
        {
            var comment = _questionCommentRepository.GetById(Id);
            if (isActive)
                comment.LikeCount++;
            else
                comment.LikeCount--;
            await _questionCommentRepository.Update(comment);
            return true;
        }

        private async Task<bool> LikeCountActivity(int Id, bool isActive)
        {
            return true;
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

        public LikeQuestion? GetUserLikedQuestion(int questionId, string userId)
        {
            var a=_mobUniDbContext.LikeQuestion.Where(question => question.QuestionId == questionId && question.IsActive && question.UserId==userId).FirstOrDefault();
            return a;
        }

        public List<LikeQuestion> GetLiked()
        {
            return _mobUniDbContext.LikeQuestion.Where(question => question.IsActive).OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public List<LikeQuestion> GetLikedByUserId(string userId)
        {
            return _mobUniDbContext.LikeQuestion.Where(question => question.UserId == userId && question.IsActive).Include(likeQuestion=>likeQuestion.User).OrderByDescending(t => t.UpdatedTime).ToList();
        }

        public List<int?> GetUserLikedComments(string userId, int id, bool isQuestionComment) // isQuestionComment= false search for ActivityComments
        {
            if (isQuestionComment)
            {
                return _mobUniDbContext.LikeQuestion.Where(comment => comment.QuestionComment.QuestionId == id && comment.IsActive && comment.UserId == userId).Select(comment => comment.QuestionCommentId).ToList();
            }
            else
            {
                return _mobUniDbContext.LikeQuestion.Where(comment => comment.QuestionComment.ActivityId == id && comment.IsActive && comment.UserId == userId).Select(comment => comment.QuestionCommentId).ToList();
            }
        }

    }
}
