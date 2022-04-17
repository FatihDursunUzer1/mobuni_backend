﻿using System;
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
        public User? GetById(string id)
        {
            return _mobUniDbContext.Set<User>().FirstOrDefault(x => x.Id == id);
        }
    }
}

