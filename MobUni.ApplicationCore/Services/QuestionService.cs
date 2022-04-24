using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Services
{
    public class QuestionService : IQuestionService
    {
        public Task<IDataResult<QuestionDTO>> Add(CreateQuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<QuestionDTO>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<QuestionDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<QuestionDTO>> Update(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

