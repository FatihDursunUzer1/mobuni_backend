using System;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class EfRepositoryBase<T>:IRepository<T> where T :class
	{
		private readonly MobUniDbContext _mobUniDbContext;

        public EfRepositoryBase(MobUniDbContext mobUniDbContext)
        {
            _mobUniDbContext = mobUniDbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _mobUniDbContext.AddAsync<T>(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity) { _mobUniDbContext.Remove<T>(entity);
            await _mobUniDbContext.SaveChangesAsync();
            return true;
        }

       

        public List<T> GetAll() => _mobUniDbContext.Set<T>().ToList();

        public async Task<T> GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

