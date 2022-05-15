using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
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
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;

        public UniversityService(IUniversityRepository universityRepository,IMapper mapper)
        {
            _universityRepository= universityRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<UniversityDTO>> Add(CreateUniversityDTO dto)
        {
            var university = _mapper.Map<University>(dto);
            university.CreateObject();
            await _universityRepository.Add(university);
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<University, UniversityDTO>(university));
        }

        public Task<bool> Delete(UniversityDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<UniversityDTO>>> GetAll()
        {
            return new SuccessDataResult<List<UniversityDTO>>(_mapper.Map<List<University>, List<UniversityDTO>>(await _universityRepository.GetAll()));
        }

        public IDataResult<UniversityDTO> GetById(int id)
        {
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<UniversityDTO>(_universityRepository.GetById(id)));
        }

        public async Task<IDataResult<UniversityDTO>> Update(UniversityDTO dto)
        {
           var university=_mapper.Map<University>(dto);
            var dbUniversity = _universityRepository.GetById(dto.Id);
            university.CreatedTime=dbUniversity.CreatedTime;
            university.UpdatedTime = DateTime.Now;
            await _universityRepository.Update(university, dto.Id);
            return new SuccessDataResult<UniversityDTO>(_mapper.Map<UniversityDTO>(university));
        }
    }
}
