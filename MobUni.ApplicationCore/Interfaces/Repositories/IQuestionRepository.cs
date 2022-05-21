using System;
using MobUni.ApplicationCore.Entities.QuestionAggregate;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
	public interface IQuestionRepository:IRepository<Question>
	{
		int GetQuestionCountByUniversityId(int universityId);
		IQueryable<Question> GetAllQuestions();
		IQueryable<Question> GetAllQuestionsByUniversityId(int universityId);
		Task<bool> LikeCount(int questionId, bool isActive);
		Task<bool> LikeOrDislikeQuestion(int questionId, string userId);
        Task<List<Question>> GetByUserId(string userId);

		void CountComment(int questionId);
    }
}

