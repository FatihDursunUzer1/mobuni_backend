using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Pagination;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;

namespace MobUni.ApplicationCore.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private  readonly IStorage _storage;
        private readonly IHttpContextAccessor _contextAccessor;

        public QuestionService(IMapper mapper, IUnitOfWork unitOfWork, IStorage storage, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _storage = storage;
            _contextAccessor = contextAccessor;
        }

        public async Task<IDataResult<QuestionDTO>> Add( CreateQuestionDTO dto,string? userId=null)
        {
            try
            {
                var question = _mapper.Map<Question>(dto);
                if (userId is not null)
                    question.UserId = userId;
                await _unitOfWork.Questions.Add(question, q => q.User, q => q.University);

                if (dto.Image != null)
                {
                    var path = await _storage.UploadQuestionImage(dto.Image, question.Id);
                    question.Image = path;
                    await _unitOfWork.Questions.Update(question, question.Id);

                }
                await _unitOfWork.Save();
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

        public IDataResult<List<QuestionDTO>> GetAll()
        {
            // var questionDtos = _mapper.Map<List<QuestionDTO>>(await _unitOfWork.Questions.GetAll());
            var questionDtos = _mapper.Map<List<QuestionDTO>>( _unitOfWork.Questions.GetAllQuestions().ToListAsync());
            CheckLikedQuestions(questionDtos);
            return new SuccessDataResult<List<QuestionDTO>>(questionDtos);
        }

        public IDataResult<QuestionDTO> GetById(int id)
        {
            var questionDTO = _mapper.Map<Question, QuestionDTO>(_unitOfWork.Questions.GetById(id));
            CheckLikedQuestion(questionDTO);
            return new SuccessDataResult<QuestionDTO>(questionDTO);
        }

        public IDataResult<List<QuestionDTO>> GetByUniversityId(int universityId)
        {
            var questionDtos = _mapper.Map<List<QuestionDTO>>(_unitOfWork.Questions.GetAll(question=>question.UniversityId==universityId));
            CheckLikedQuestions(questionDtos);
            return new SuccessDataResult<List<QuestionDTO>>(questionDtos);
        }

        public IDataResult<int> GetQuestionCountByUniversityId(int universityId,DateTime? dateTime=null)
        {
            var questionCount = _unitOfWork.Questions.GetQuestionCountByUniversityId(universityId,dateTime);
            return new SuccessDataResult<int>(questionCount);
        }
        public IDataResult<List<QuestionDTO>> GetMyQuestions(string userId)
        {
            return new SuccessDataResult<List<QuestionDTO>>(_mapper.Map<List<QuestionDTO>>( _unitOfWork.Questions.GetAll(question => question.UserId == userId)));
        }

        public async Task<IDataResult<bool>> LikeQuestion(int questionId, string? userId=null)
        {
            try
            {
                // await _likeQuestionRepository.LikeQuestion(questionId, userId);
               await _unitOfWork.Questions.LikeOrDislikeQuestion(questionId, userId);
                await _unitOfWork.Save();
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
            var dbQuestion = _unitOfWork.Questions.GetById(dto.Id);
            question.UpdatedTime =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            question.CreatedTime = dbQuestion.CreatedTime;

            question=await _unitOfWork.Questions.Update(question, question.Id);
            await _unitOfWork.Save();
            return new SuccessDataResult<QuestionDTO>(_mapper.Map<Question, QuestionDTO>(question));
        }
        private void CheckLikedQuestion(QuestionDTO questionDTO)
        {
            if(_unitOfWork.Likes.GetUserLikedQuestion(questionDTO.Id,_contextAccessor.HttpContext.Items["UserId"]?.ToString()) != null)
                questionDTO.IsLiked=true;
        }
        private void CheckLikedQuestions(List<QuestionDTO> questionDTOs)
        {
            questionDTOs.ForEach(x => { if (_unitOfWork.Likes.GetUserLikedQuestion(x.Id, _contextAccessor.HttpContext.Items["UserId"]?.ToString()) != null)
                    x.IsLiked = true;
                else x.IsLiked = false;
                });
        }

         public IDataResult<List<LikeQuestionDTO>> GetMyLikedQuestions(string userId)
        {
            var likeQuestion = _unitOfWork.Likes.GetLikedByUserId(userId);
            return new SuccessDataResult<List<LikeQuestionDTO>>(_mapper.Map<List<LikeQuestionDTO>>(likeQuestion));
        }

        public IDataResult<List<QuestionDTO>> GetQuestionsByUserId(string userId)
        {
            var questions = _unitOfWork.Questions.GetByUserId(userId);
            var questionDTOS = _mapper.Map<List<QuestionDTO>>(questions);
            CheckLikedQuestions(questionDTOS);
            return new SuccessDataResult<List<QuestionDTO>>(questionDTOS);
        }

        public IDataResult<PaginatedList<QuestionDTO>> GetQuestionsByUserIdPagination(string userId, PaginationQuery paginationQuery)
        {
            var questions = _unitOfWork.Questions.GetByUserId(userId);
            var paginatedList = PaginatedList<QuestionDTO>.CreateAsync(_mapper.Map<List<Question>, List<QuestionDTO>>(questions), paginationQuery.PageIndex, paginationQuery.PageSize);
            CheckLikedQuestions(paginatedList.Items);
            return new SuccessDataResult<PaginatedList<QuestionDTO>>(paginatedList);
        }

        public IDataResult<PaginatedList<QuestionDTO>> GetQuestionsByUniversityIdPagination(int universityId, PaginationQuery paginationQuery)
        {
            var questions = _unitOfWork.Questions.GetAll(question => question.UniversityId == universityId);
            var paginatedList = PaginatedList<QuestionDTO>.CreateAsync(_mapper.Map<List<Question>, List<QuestionDTO>>(questions), paginationQuery.PageIndex, paginationQuery.PageSize);
            CheckLikedQuestions(paginatedList.Items);
            return new SuccessDataResult<PaginatedList<QuestionDTO>>(paginatedList);
        }

    }
}

