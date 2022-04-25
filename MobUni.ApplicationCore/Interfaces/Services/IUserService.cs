using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IUserService:IService<UserDTO,CreateUserDTO>
	{
		IDataResult<UserDTO> GetById(string userId);
		IDataResult<Token> Login(CreateUserDTO userDto);
        Task<IDataResult<Token>> Register(CreateUserDTO userDto);
    }
}

