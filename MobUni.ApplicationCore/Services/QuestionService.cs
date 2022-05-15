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
        private readonly ILikeQuestionRepository _likeQuestionRepository;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, ILikeQuestionRepository likeQuestionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _likeQuestionRepository = likeQuestionRepository;
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
            var questionDtos = _mapper.Map<List<Question>, List<QuestionDTO>>(await _questionRepository.GetAll());
            CheckLikedQuestions(questionDtos);
            return new SuccessDataResult<List<QuestionDTO>>(questionDtos);
        }

        public IDataResult<QuestionDTO> GetById(int id)
        {
            var questionDTO = _mapper.Map<Question, QuestionDTO>(_questionRepository.GetById(id));
            CheckLikedQuestion(questionDTO);
            return new SuccessDataResult<QuestionDTO>();
        }

        public async Task<IDataResult<bool>> LikeQuestion(int questionId, string userId)
        {
            return new SuccessDataResult<bool>(await _likeQuestionRepository.ChangeStatus(questionId, userId));
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
        private void CheckLikedQuestion(QuestionDTO questionDTO)
        {
            if(_likeQuestionRepository.GetByQuestionId(questionDTO.Id)!=null)
                questionDTO.IsLiked=true;
        }
        private void CheckLikedQuestions(List<QuestionDTO> questionDTOs)
        {
            questionDTOs.ForEach(x => { if (_likeQuestionRepository.GetByQuestionId(x.Id) != null)
                x.IsLiked = true;
                });
        }
    }
}

