using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
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
        public async Task<DepartmentDTO> Add(CreateDepartmentDTO dto)
        {
            var department = _mapper.Map<CreateDepartmentDTO, Department>(dto);
            department.CreateObject();
            return _mapper.Map<Department, DepartmentDTO>(await _departmentRepository.Add(department));

        }

        public Task<bool> Delete(DepartmentDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DepartmentDTO>> GetAll()
        {
            return _mapper.Map<List<Department>, List<DepartmentDTO>>(await _departmentRepository.GetAll());
        }

        public DepartmentDTO GetById(int id)
        {
            return _mapper.Map<Department,DepartmentDTO>(_departmentRepository.GetById(id));
        }

        public async Task<DepartmentDTO> Update(CreateDepartmentDTO dto)
        {
            var department = _mapper.Map<CreateDepartmentDTO, Department>(dto);
            
            return _mapper.Map<Department, DepartmentDTO>(await _departmentRepository.Update(department));
        }
    }
}
