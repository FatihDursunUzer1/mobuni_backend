using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Interfaces;
using MobUni.Infrastructure.Data.Contexts;

namespace MobUni.Infrastructure.Repositories
{
	public class EfRepositoryBase<T>:IRepository<T> where T :BaseEntity
	{
		protected readonly MobUniDbContext _mobUniDbContext;

        public EfRepositoryBase(MobUniDbContext mobUniDbContext)
        {
            _mobUniDbContext = mobUniDbContext;
        }

        public async Task<T> Add(T entity, params Expression<Func<T, object>>[] includes)
        {
            var newEntry = _mobUniDbContext.Set<T>().Add(entity);
            //await _mobUniDbContext.SaveChangesAsync();    // trip to database

            await ApplyIncludes(includes, newEntry);
            return newEntry.Entity;
        }
        public async Task<T> Add(T entity)
        {
            _mobUniDbContext.Set<T>().Add(entity);
            //await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }
        private static async Task ApplyIncludes(Expression<Func<T, object>>[] includes, Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> newEntry)
        {
            foreach (var navProp in includes)
            {
                string propertyName = navProp.GetPropertyAccess().Name;

                if (newEntry.Navigation(propertyName).Metadata.IsCollection)
                {
                    await newEntry.Collection(propertyName).LoadAsync();    // trip to database
                }
                else
                {
                    await newEntry.Reference(propertyName).LoadAsync();    // trip to database
                }
            }
        }

        public async Task<bool> Delete(T entity) { _mobUniDbContext.Remove<T>(entity);
           // await _mobUniDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> exp=null,int? pageSize=null, int? pageIndex=null)
        {
            if (exp != null)
            {
                if(pageSize!=null && pageIndex!=null)
                return _mobUniDbContext.Set<T>().Where(exp).OrderByDescending(t => t.CreatedTime).Skip((int)pageIndex).Take((int)pageSize).ToList();
                return _mobUniDbContext.Set<T>().Where(exp).OrderByDescending(t => t.CreatedTime).ToList();
            }
            if (pageSize != null && pageIndex != null)
                return _mobUniDbContext.Set<T>().OrderByDescending(t => t.CreatedTime).Skip((int)pageIndex).Take((int)pageSize).ToList();
            return _mobUniDbContext.Set<T>().OrderByDescending(t => t.CreatedTime).ToList();
        }

        public  T GetById(int id)
        {
            return _mobUniDbContext.Set<T>().Find(id);
        }

        public async Task<T> Update(T entity,int entityId)
        {
            /* T exist = _mobUniDbContext.Set<T>().Find();
            _mobUniDbContext.Entry(exist).CurrentValues.SetValues(entity);*/
            _mobUniDbContext.Attach(entity);
            _mobUniDbContext.Entry(entity).State = EntityState.Modified;
            //await _mobUniDbContext.SaveChangesAsync();
            return entity;
        }

        /*  public async Task<T> AddOrUpdateAsync(T entity)
         {
             if(!_mobUniDbContext.Set<T>().Local.Any(i=>EqualityComparer<int>.Default.Equals(i.Id, entity.Id)))
             {
                 await Update(entity);
             }
             else
             {
                 await Add(entity);
             }
             return entity;

         } */
    }
}

