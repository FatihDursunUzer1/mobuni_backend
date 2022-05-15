using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IQuestionService:IService<QuestionDTO,CreateQuestionDTO>
	{
		public Task<IDataResult<bool>> LikeQuestion(int questionId, string userId);
	}
}

