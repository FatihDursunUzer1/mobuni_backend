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
           
            //_mobUniDbContext.Set<User>().Attach(entity);
            var user = GetById(entity.Id);
            user.UpdatedTime = DateTime.Now;
            if(entity.Email != null)
                user.Email = entity.Email;
            if(entity.UserName != null)
                user.UserName = entity.UserName;
            if(entity.Name!=null)
                user.Name=entity.Name;
            if(entity.PhoneNumber!=null)
                user.PhoneNumber= entity.PhoneNumber;
            if(entity.Surname!=null)
                user.Surname=entity.Surname;
            if(entity.UniversityId!=null)
                user.UniversityId=entity.UniversityId;
            if(entity.DepartmentId!=null)
                user.DepartmentId=entity.DepartmentId;

            _mobUniDbContext.Users.Update(user);
 
            return user;
        }
    }
}

