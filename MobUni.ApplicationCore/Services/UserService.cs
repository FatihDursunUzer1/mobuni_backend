using System;
using AutoMapper;
using MobUni.ApplicationCore.DTOs;
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

        public async Task<UserDTO> Add(UserDTO dto)
        {
            var user = _mapper.Map<UserDTO, User>(dto);
            return _mapper.Map<User,UserDTO>(await _userRepository.Add(user));
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

        public Task<UserDTO> Update(UserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

