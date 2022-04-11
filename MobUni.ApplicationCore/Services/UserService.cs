using System;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.Interfaces;

namespace MobUni.ApplicationCore.Services
{
	public class UserService:IUserService
	{
		public UserService()
		{
		}

        public Task<UserDTO> Add(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Update(UserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

