using System;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class UserRepository: EfRepositoryBase<User>,IUserRepository
	{
        public UserRepository(MobUniDbContext mobUniDbContext):base(mobUniDbContext)
        {

        }
	}
}

