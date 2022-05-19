using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private  readonly IStorage _storage;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository, ILikeQuestionRepository likeQuestionRepository,IStorage storage)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _likeQuestionRepository = likeQuestionRepository;
            _storage = storage;
        }
        public async Task<IDataResult<QuestionDTO>> Add( CreateQuestionDTO dto,string? userId=null)
        {
            try
            {
                var question = _mapper.Map<Question>(dto);
                if (userId is not null)
                    question.UserId = userId;
                await _questionRepository.Add(question, q => q.User, q => q.University);

                if (dto.Image != null)
                {
                    var path = await _storage.UploadQuestionImage(dto.Image, question.Id);
                    question.Image = path;
                    await _questionRepository.Update(question);

                }
                return new SuccessDataResult<QuestionDTO>(_mapper.Map<Question, QuestionDTO>(question));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(QuestionDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<QuestionDTO>>> GetAll()
        {
            // var questionDtos = _mapper.Map<List<QuestionDTO>>(await _questionRepository.GetAll());
            var questionDtos = _mapper.Map<List<QuestionDTO>>( await _questionRepository.GetAllQuestions().ToListAsync());
            CheckLikedQuestions(questionDtos);
            return new SuccessDataResult<List<QuestionDTO>>(questionDtos);
        }

        public IDataResult<QuestionDTO> GetById(int id)
        {
            var questionDTO = _mapper.Map<Question, QuestionDTO>(_questionRepository.GetById(id));
            CheckLikedQuestion(questionDTO);
            return new SuccessDataResult<QuestionDTO>(questionDTO);
        }

        public async Task<IDataResult<List<QuestionDTO>>> GetByUniversityId(int universityId)
        {
            var questionDtos = _mapper.Map<List<QuestionDTO>>(await _questionRepository.GetAllQuestionsByUniversityId(universityId).ToListAsync());
            CheckLikedQuestions(questionDtos);
            return new SuccessDataResult<List<QuestionDTO>>(questionDtos);
        }

        public IDataResult<int> GetQuestionCountByUniversityId(int universityId)
        {
            var questionCount = _questionRepository.GetQuestionCountByUniversityId(universityId);
            return new SuccessDataResult<int>(questionCount);
        }

        public async Task<IDataResult<List<QuestionDTO>>> GetMyQuestions(string userId)
        {
            return new SuccessDataResult<List<QuestionDTO>>(_mapper.Map<List<QuestionDTO>>(await _questionRepository.GetAll(question => question.UserId == userId)));
        }

        public async Task<IDataResult<bool>> LikeQuestion(int questionId, string? userId=null)
        {
            try
            {
                await _likeQuestionRepository.LikeQuestion(questionId, userId);
                await _questionRepository.LikeCount(questionId);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IDataResult<QuestionDTO>> Update(QuestionDTO dto)
        {
            var question = _mapper.Map<Question>(dto);
            var dbQuestion = _questionRepository.GetById(dto.Id);
            question.UpdatedTime = DateTime.Now;
            question.CreatedTime = dbQuestion.CreatedTime;

            await _questionRepository.Update(question);
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

