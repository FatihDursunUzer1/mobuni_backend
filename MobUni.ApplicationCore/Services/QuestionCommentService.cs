using AutoMapper;
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

        public QuestionCommentService(IQuestionCommentRepository questionCommentRepository, IMapper mapper, ILikeQuestionRepository likeRepository)
        {
            _questionCommentRepository = questionCommentRepository;
            _mapper = mapper;
            _likeRepository = likeRepository;
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
                await _questionCommentRepository.Add(questionComment, questionComment => questionComment.Question, questionComment => questionComment.User);
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
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        public IDataResult<QuestionCommentDTO> GetById(int id)
        {
            //Check am I Liked?
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(_questionCommentRepository.GetById(id)));
        }

        public async Task<IDataResult<List<QuestionCommentDTO>>> GetByQuestionId(int questionId)
        {
            //Check am I Liked?
            var a = await _questionCommentRepository.GetAll(question=>question.QuestionId==questionId);
            List<QuestionCommentDTO> questionComments = _mapper.Map<List<QuestionComment>, List<QuestionCommentDTO>>(a);
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        public async Task<IDataResult<QuestionCommentDTO>> Update(QuestionCommentDTO dto)
        {
            var questionComment= _mapper.Map<QuestionComment>(dto);
            questionComment.UpdatedTime = DateTime.Now;
            await _questionCommentRepository.Update(questionComment);
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(questionComment));
        }
    }
}
