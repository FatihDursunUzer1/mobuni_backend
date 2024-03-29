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
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UniversityService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<UniversityDTO>> Add(CreateUniversityDTO dto,string? userId=null)
        {
            var university = _mapper.Map<University>(dto);
            university.CreateObject();
            await _unitOfWork.Universities.Add(university);
            await _unitOfWork.Save();
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<University, UniversityDTO>(university));
        }

        public Task<bool> Delete(UniversityDTO dto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UniversityDTO>> GetAll()
        {
            return new SuccessDataResult<List<UniversityDTO>>(_mapper.Map<List<University>, List<UniversityDTO>>(_unitOfWork.Universities.GetAll()));
        }

        public IDataResult<UniversityDTO> GetById(int id)
        {
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<UniversityDTO>(_unitOfWork.Universities.GetById(id)));
        }

        public async Task<IDataResult<UniversityDTO>> Update(UniversityDTO dto)
        {
           var university=_mapper.Map<University>(dto);
            var dbUniversity = _unitOfWork.Universities.GetById(dto.Id);
            university.CreatedTime=dbUniversity.CreatedTime;
            university.UpdatedTime =  DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            university= await _unitOfWork.Universities.Update(university, university.Id);
            await _unitOfWork.Save();
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<UniversityDTO>(university));
        }
    }
}
