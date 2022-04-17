using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Services
{
	public class QuestionService:IQuestionService
	{
		public QuestionService()
		{
		}

        public Task<QuestionDTO> Add(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDTO> Update(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

