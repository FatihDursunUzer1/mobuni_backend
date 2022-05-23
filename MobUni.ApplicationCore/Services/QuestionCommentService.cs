using AutoMapper;
using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.QuestionAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Services
{
    public class QuestionCommentService : IQuestionCommentService
    {
        private ILikeQuestionRepository _likeRepository;
        private readonly IQuestionCommentRepository _questionCommentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _userId;
        private readonly IQuestionRepository _questionRepository;
        private readonly IActivityRepository _activityRepository;

        public QuestionCommentService(IQuestionCommentRepository questionCommentRepository, IMapper mapper,
            ILikeQuestionRepository likeRepository, IHttpContextAccessor httpContextAccessor,IActivityRepository activityRepository, IQuestionRepository questionRepository)
        {
            _questionCommentRepository = questionCommentRepository;
            _mapper = mapper;
            _likeRepository = likeRepository;
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext.Items["UserId"].ToString();
            _activityRepository = activityRepository;
            _questionRepository = questionRepository;
        }
        public async Task<IDataResult<QuestionCommentDTO>> Add(CreateQuestionCommentDTO dto, string? userId = null)
        {
            var questionComment = _mapper.Map<QuestionComment>(dto);
            if(userId != null)
                questionComment.UserId = userId;
            
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(questionComment));
        }

        public async Task<IDataResult<bool>> AddComment(CreateQuestionCommentDTO dto, string? userId=null)
        {
            try
            {
                var questionComment = _mapper.Map<QuestionComment>(dto);
                if (userId != null)
                    questionComment.UserId = userId;
                await _questionCommentRepository.Add(questionComment, questionComment => questionComment.Question, questionComment => questionComment.User,questionComment=>questionComment.Activity);
                if (questionComment.Question != null)
                    _questionRepository.CountComment((int)questionComment.QuestionId);
                else if (questionComment.Activity != null)
                    _activityRepository.CountComment((int)questionComment.ActivityId);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(QuestionCommentDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<QuestionCommentDTO>>> GetAll()
        {

            //Check am I Liked?
            var a = await _questionCommentRepository.GetAll();
            List<QuestionCommentDTO> questionComments = _mapper.Map<List<QuestionComment>, List<QuestionCommentDTO>>(a);
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        public async Task<IDataResult<List<QuestionCommentDTO>>> GetByActivityId(int activityId)
        {

            //Check am I Liked?
            var a = await _questionCommentRepository.GetAll(question => question.ActivityId==activityId);
            List<QuestionCommentDTO> questionComments = _mapper.Map<List<QuestionComment>, List<QuestionCommentDTO>>(a);
            CheckLikedComments(questionComments,activityId, _userId, false);
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        public IDataResult<QuestionCommentDTO> GetById(int id)
        {
            //Check am I Liked?
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(_questionCommentRepository.GetById(id)));
        }

        public async Task<IDataResult<List<QuestionCommentDTO>>> GetByQuestionId(int questionId)
        {
            var a = await _questionCommentRepository.GetAll(question=>question.QuestionId==questionId);
            List<QuestionCommentDTO> questionComments = _mapper.Map<List<QuestionComment>, List<QuestionCommentDTO>>(a);
            CheckLikedComments(questionComments, questionId, _userId, true);
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        private void CheckLikedComments(List<QuestionCommentDTO> questionComments, int id, string userId, bool isQuestionComment)
        {
            var likes = _likeRepository.GetUserLikedComments(_userId,id, isQuestionComment);
            foreach (var comment in questionComments)
            {
                foreach(var like in likes)
                {
                    if (comment.Id == like)
                        comment.IsLiked = true;
                }
            }
        }

        public async Task<IDataResult<QuestionCommentDTO>> Update(QuestionCommentDTO dto)
        {
            var questionComment= _mapper.Map<QuestionComment>(dto);
            questionComment.UpdatedTime =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            await _questionCommentRepository.Update(questionComment, questionComment.Id);
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(questionComment));
        }
    }
}
