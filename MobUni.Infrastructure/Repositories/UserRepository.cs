using System;
using Microsoft.EntityFrameworkCore;
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
        public User? GetByEmailOrUserName(string emailOrUserName)
        {
            var user = _mobUniDbContext.Set<User>().FirstOrDefault(u => u.Email == emailOrUserName);
            if(user ==null )
                user= _mobUniDbContext.Set<User>().FirstOrDefault(u=>u.UserName==emailOrUserName);
            return user;
        }
        public User? GetByEmail(string? email)
        {
            return _mobUniDbContext.Set<User>().FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(string id)
        {
            return _mobUniDbContext.Set<User>().FirstOrDefault(u=>u.Id == id);
        }

        public User? GetByIdAsNoTracking(string id)
        {
            return _mobUniDbContext.Set<User>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public User? GetByUserName(string userName)
        {
            return _mobUniDbContext.Set<User>().FirstOrDefault(x=>x.UserName == userName);
        }

        public async Task<User> UpdateAsync(User entity)
        {
           /* _mobUniDbContext.Set<User>().Attach(entity);
            _mobUniDbContext.Entry(await _mobUniDbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == entity.Id)).CurrentValues.SetValues(entity); */
          var c= _mobUniDbContext.Update<User>(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }
    }
}

