using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
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
    public class LikeService : ILikeService
    {
        private readonly IPushNotification _pushNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(IMapper mapper, IUnitOfWork unitOfWork, IPushNotification pushNotification)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pushNotification = pushNotification;
        }
        public async Task<IDataResult<bool>> Like(CreateLikeDTO dto, string? userId = null)
        {
            var like = new LikeQuestion();
            if (dto.TableType == 2)
                like.QuestionCommentId = dto.Id;
            else if (dto.TableType == 1)
                like.QuestionId = dto.Id;
            like.UserId = userId;
            like.IsActive = true;
            bool value = await _unitOfWork.Likes.LikeOrDislike(dto.TableType, dto.Id, userId); // beğeni geri çekildeyse false dönecek.
            await _unitOfWork.Save();
            if (value==true)
            {
                var user = _unitOfWork.Users.GetById(userId);
                if (dto.TableType == 2)
                {
                    var questionComment = _unitOfWork.Comments.GetById(dto.Id);
                   
                    if (questionComment.ActivityId is not null)
                    {
                        await _pushNotification.SendActivityCommentLikeNotification(userId, questionComment.UserId, (int)questionComment.ActivityId, user.FullName, questionComment.Content);
                    }
                    else if (questionComment.QuestionId is not null)
                    {
                        await _pushNotification.SendQuestionCommentLikeNotification(userId, questionComment.UserId, (int)questionComment.QuestionId, user.FullName, questionComment.Content);
                    }
                }
                else
                {
                    var question = _unitOfWork.Questions.GetById(dto.Id);
                    await _pushNotification.SendQuestionLikeNotification(userId,question.UserId, question.Id,user.FullName, question.Text);
                }
                
            }
           
            return new SuccessDataResult<bool>(value);
        }

       

        public Task<bool> Delete(LikeDTO dto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<LikeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<LikeDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<LikeDTO>> Update(LikeDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<IDataResult<LikeDTO>> IService<LikeDTO, CreateLikeDTO>.Add(CreateLikeDTO dto, string? userId)
        {
            throw new NotImplementedException();
        }
    }
}
