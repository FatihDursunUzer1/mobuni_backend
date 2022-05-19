using System;
using MobUni.ApplicationCore.Entities.QuestionAggregate;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
	public interface IQuestionRepository:IRepository<Question>
	{
		int GetQuestionCountByUniversityId(int universityId);
		IQueryable<Question> GetAllQuestions();
		IQueryable<Question> GetAllQuestionsByUniversityId(int universityId);
	}
}

