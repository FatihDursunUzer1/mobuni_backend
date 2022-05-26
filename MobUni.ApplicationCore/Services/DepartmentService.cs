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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           
        }
        public async Task<IDataResult<DepartmentDTO>> Add(CreateDepartmentDTO dto, string? userId=null)
        {
            var department = _mapper.Map<CreateDepartmentDTO, Department>(dto);
            department.CreateObject();
            department= await _unitOfWork.Departments.Add(department);
            await _unitOfWork.Save();
            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(department)); 

        }
        public Task<bool> Delete(DepartmentDTO dto)
        {
            throw new NotImplementedException();
        }

        public  IDataResult<List<DepartmentDTO>> GetAll()
        {
            return new SuccessDataResult<List<DepartmentDTO>>(_mapper.Map<List<Department>, List<DepartmentDTO>>( _unitOfWork.Departments.GetAll()));
        }

        public IDataResult<DepartmentDTO> GetById(int id)
        {
            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(_unitOfWork.Departments.GetById(id)));
        }

        public async Task<IDataResult<DepartmentDTO>> Update(DepartmentDTO dto)
        {
            var department = _mapper.Map<DepartmentDTO, Department>(dto);
            department = await _unitOfWork.Departments.Update(department, department.Id);
            await _unitOfWork.Save();
            return new SuccessDataResult<DepartmentDTO>(_mapper.Map<Department, DepartmentDTO>(department));
        }

    }
}
