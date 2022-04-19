using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;

namespace MobUni.ApplicationCore.Services
{
	public class UserService:IUserService
	{
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
		public UserService(IUserRepository userRepository,IMapper mapper)
		{
            _userRepository = userRepository;
            _mapper = mapper;
		}

        public async Task<UserDTO> Add(CreateUserDTO dto)
        {
            var user = _mapper.Map<CreateUserDTO, User>(dto);
            user.CreateUserTime();
            await _userRepository.Add(user);
            return _mapper.Map<User, UserDTO>(_userRepository.GetById(user.Id));
        }

        public Task<bool> Delete(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task< List<UserDTO>> GetAll()
        {
            return _mapper.Map<List<User>,List<UserDTO>>(await _userRepository.GetAll());
        }

        public  UserDTO GetById(string userId)
        {
            return _mapper.Map<User, UserDTO>( _userRepository.GetById(userId));
        }

        public async Task<UserDTO> Update(UserDTO dto)
        {
            var dtoUser = _mapper.Map<User>(dto);
            var user = _userRepository.GetByIdAsNoTracking(dto.Id);
            dtoUser.UpdatedTime = DateTime.Now;
            dtoUser.CreatedTime = user?.CreatedTime;

            await _userRepository.UpdateAsync(dtoUser);
            return _mapper.Map<User, UserDTO>(dtoUser);
        }

        UserDTO IService<UserDTO, CreateUserDTO>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

