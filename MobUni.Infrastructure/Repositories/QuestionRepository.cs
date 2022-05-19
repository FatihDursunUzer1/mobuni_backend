using System;
using Microsoft.EntityFrameworkCore;
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
            return _mobUniDbContext.Questions.Include(question => question.User).AsQueryable();
        }

        public IQueryable<Question> GetAllQuestionsByUniversityId(int universityId)
        {
            return GetAllQuestions().Where(question => question.UniversityId == universityId);
        }

        public async Task<bool> LikeCount(int questionId)
        {
            var question=GetById(questionId);
            question.LikeCount++;
            await Update(question);
            return true;
        }
    }
}

