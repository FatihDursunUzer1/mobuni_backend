using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;

namespace MobUni.ApplicationCore.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IMapper mapper,IQuestionRepository questionRepository)
        {
           _mapper = mapper;
           _questionRepository = questionRepository;
        }
        public async Task<IDataResult<QuestionDTO>> Add(CreateQuestionDTO dto)
        {
            var question= _mapper.Map<Question>(dto);
            await _questionRepository.Add(question);
            return new SuccessDataResult<QuestionDTO>(_mapper.Map<Question, QuestionDTO>(question));
        }

        public Task<bool> Delete(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<QuestionDTO>>> GetAll()
        {
            return new SuccessDataResult<List<QuestionDTO>>(_mapper.Map<List<Question>, List<QuestionDTO>>(await _questionRepository.GetAll()));
        }

        public IDataResult<QuestionDTO> GetById(int id)
        {
            return new SuccessDataResult<QuestionDTO>(_mapper.Map<Question,QuestionDTO>(_questionRepository.GetById(id)));
        }

        public async Task<IDataResult<QuestionDTO>> Update(QuestionDTO dto)
        {
            var question = _mapper.Map<Question>(dto);
            var dbQuestion = _questionRepository.GetById(dto.Id);
            question.UpdatedTime = DateTime.Now;
            question.CreatedTime = dbQuestion.CreatedTime;

            await _questionRepository.Update(question, question.Id);
            return new SuccessDataResult<QuestionDTO>(_mapper.Map<Question, QuestionDTO>(question));
        }
    }
}

