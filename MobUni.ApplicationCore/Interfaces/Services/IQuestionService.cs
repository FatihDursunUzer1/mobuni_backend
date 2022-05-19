using System;
using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IQuestionService:IService<QuestionDTO,CreateQuestionDTO>
	{
		public Task<IDataResult<bool>> LikeQuestion(int questionId, string? userId=null);
		Task<IDataResult<List<QuestionDTO>>> GetByUniversityId(int universityId);
		Task<IDataResult<List<QuestionDTO>>> GetMyQuestions(string userId);
		Task<IDataResult<List<QuestionDTO>>> GetQuestionsByUserId(string userId);
		IDataResult<List<LikeQuestionDTO>> GetMyLikedQuestions(string userId);

		IDataResult<int> GetQuestionCountByUniversityId(int universityId);
		//public Task<IDataResult<QuestionDTO>> Add(CreateQuestionDTO dto, IFormFile file, string? userId = null);
	}
}

