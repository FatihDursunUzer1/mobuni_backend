﻿using AutoMapper;
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
        private readonly ILikeQuestionRepository _likeQuestionRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeQuestionRepository likeQuestionRepository,IMapper mapper)
        {
            _likeQuestionRepository = likeQuestionRepository;
            _mapper = mapper;
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
            return new SuccessDataResult<bool>(await _likeQuestionRepository.LikeOrDislike(dto.TableType, dto.Id, userId));
        }

       

        public Task<bool> Delete(LikeDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<LikeDTO>>> GetAll()
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