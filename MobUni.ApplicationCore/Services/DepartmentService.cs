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
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IMapper mapper,IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository= departmentRepository;
        }
        public async Task<IDataResult<DepartmentDTO>> Add(CreateDepartmentDTO dto, string? userId=null)
        {
            var department = _mapper.Map<CreateDepartmentDTO, Department>(dto);
            department.CreateObject();
            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(await _departmentRepository.Add(department))); 

        }
        public Task<bool> Delete(DepartmentDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<DepartmentDTO>>> GetAll()
        {
            return new SuccessDataResult<List<DepartmentDTO>>(_mapper.Map<List<Department>, List<DepartmentDTO>>(await _departmentRepository.GetAll()));
        }

        public IDataResult<DepartmentDTO> GetById(int id)
        {
            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(_departmentRepository.GetById(id)));
        }

        public async Task<IDataResult<DepartmentDTO>> Update(DepartmentDTO dto)
        {
            var department = _mapper.Map<DepartmentDTO, Department>(dto);

            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(await _departmentRepository.Update(department)));
        }

    }
}
