using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Services
{
    public class QuestionService : IQuestionService
    {
        public Task<QuestionDTO> Add(CreateQuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public QuestionDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionDTO> Update(CreateQuestionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

