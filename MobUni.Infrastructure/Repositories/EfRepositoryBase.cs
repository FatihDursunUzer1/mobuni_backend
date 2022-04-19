using System;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class EfRepositoryBase<T>:IRepository<T> where T :class
	{
		protected readonly MobUniDbContext _mobUniDbContext;

        public EfRepositoryBase(MobUniDbContext mobUniDbContext)
        {
            _mobUniDbContext = mobUniDbContext;
        }

        public async Task<T> Add(T entity)
        {
           var a= await _mobUniDbContext.AddAsync<T>(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity) { _mobUniDbContext.Remove<T>(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return true;
        }

       

        public async Task<List<T>> GetAll()
        {
           return await _mobUniDbContext.Set<T>().ToListAsync();
        }

        public  T GetById(int id)
        {
            return _mobUniDbContext.Set<T>().Find(id);
        }

        public async Task<T> Update(T entity,int entityId)
        {
            T exist = _mobUniDbContext.Set<T>().Find(entityId);
            _mobUniDbContext.Entry(exist).CurrentValues.SetValues(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }
    }
}

