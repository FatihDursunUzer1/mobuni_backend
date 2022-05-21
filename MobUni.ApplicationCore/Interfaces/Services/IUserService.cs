using System;
using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Result.Abstract;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IUserService:IService<UserDTO,CreateUserDTO>
	{
		IDataResult<UserDTO> GetById(string userId);
		IDataResult<TokenDTO> Login(LoginUserDTO userDto);
        Task<IDataResult<TokenDTO>> Register(CreateUserDTO userDto);
		Task<IDataResult<string>> UpdateProfileImage(IFormFile image);



	}
}

