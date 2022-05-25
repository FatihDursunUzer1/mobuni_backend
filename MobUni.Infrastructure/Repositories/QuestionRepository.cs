using System;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class QuestionRepository: EfRepositoryBase<Question>,IQuestionRepository
	{
        public QuestionRepository(MobUniDbContext mobUniDbContext):base(mobUniDbContext)
        {

        }

        public int GetQuestionCountByUniversityId(int universityId)
        {
            return _mobUniDbContext.Questions.Where(question=>question.University.Id== universityId).Count();
        }

        public IQueryable<Question> GetAllQuestions()
        {
            return _mobUniDbContext.Questions.Include(question => question.User).OrderByDescending(t => t.CreatedTime).AsQueryable();
        }

        public IQueryable<Question> GetAllQuestionsByUniversityId(int universityId)
        {
            return GetAllQuestions().Where(question => question.UniversityId == universityId).OrderByDescending(t => t.CreatedTime);
        }

        public async Task<bool> LikeCount(int questionId,bool isActive)
        {
            var question=GetById(questionId);
            if(isActive)
                question.LikeCount++;
            else
                question.LikeCount--;
            await Update(question,question.Id);
            return true;
        }

        public async Task<bool> LikeOrDislikeQuestion(int questionId, string userId)
        {
            var questionLike = _mobUniDbContext.LikeQuestion.Where(question => question.QuestionId == questionId && question.UserId == userId).FirstOrDefault();
            if (questionLike != null)
            {
                questionLike.IsActive = !questionLike.IsActive;
                _mobUniDbContext.LikeQuestion.Update(questionLike);
                return await LikeCount(questionId, questionLike.IsActive);
            }
            else
            {
                var question = new LikeQuestion { QuestionId = questionId, UserId = userId, IsActive = true };
                question.CreateObject();
                await _mobUniDbContext.LikeQuestion.AddAsync(question);
                return await LikeCount(questionId,true);
            }
        }

        public async Task<List<Question>> GetByUserId(string userId)
        {
            return await _mobUniDbContext.Questions.Where(question=>question.UserId==userId).Include(question => question.User).OrderByDescending(t => t.CreatedTime).ToListAsync();
        }

        public void CountComment(int questionId)
        {
           var question= GetById(questionId);
            if (question != null)
                question.CommentCount++;
            _mobUniDbContext.Questions.Update(question);

        }
    }
}

