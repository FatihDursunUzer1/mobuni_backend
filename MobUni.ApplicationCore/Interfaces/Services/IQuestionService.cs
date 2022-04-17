using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IQuestionService:IService<QuestionDTO,CreateQuestionDTO>
	{
	}
}

