using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IUserService:IService<UserDTO,CreateUserDTO>
	{
		UserDTO GetById(string userId);
		string Login(CreateUserDTO userDto);
        Task<string> Register(CreateUserDTO userDto);
    }
}

