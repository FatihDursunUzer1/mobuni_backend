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
        private readonly IQuestionCommentRepository _questionCommentRepository;
        private readonly IMapper _mapper;

        public QuestionCommentService(IQuestionCommentRepository questionCommentRepository,IMapper mapper)
        {
            _questionCommentRepository = questionCommentRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<QuestionCommentDTO>> Add(CreateQuestionCommentDTO dto, string? userId = null)
        {
            var questionComment = _mapper.Map<QuestionComment>(dto);
            await _questionCommentRepository.Add(questionComment,questionComment=>questionComment.Question,questionComment=>questionComment.User);
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(questionComment));
        }

        public Task<bool> Delete(QuestionCommentDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<QuestionCommentDTO>>> GetAll()
        {
            var a = await _questionCommentRepository.GetAll();
            List<QuestionCommentDTO> questionComments = _mapper.Map<List<QuestionComment>, List<QuestionCommentDTO>>(a);
            return new SuccessDataResult<List<QuestionCommentDTO>>(questionComments);
        }

        public IDataResult<QuestionCommentDTO> GetById(int id)
        {
            return new SuccessDataResult<QuestionCommentDTO>(_mapper.Map<QuestionCommentDTO>(_questionCommentRepository.GetById(id)));
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
