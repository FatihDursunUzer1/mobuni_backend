using System;
using MobUni.ApplicationCore.Entities.UserAggregate;

namespace MobUni.ApplicationCore.Interfaces.Repositories
{
	public interface IUserRepository:IRepository<User>
	{
		User? GetById(string UserId);
		Task<User> UpdateAsync(User entity);
		User? GetByIdAsNoTracking(string id);
		User? GetByUserName(string userName);
	}
}

