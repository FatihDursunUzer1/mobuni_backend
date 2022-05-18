using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public T GetById(int id);
		public Task<List<T>> GetAll(Expression<Func<T, bool>> exp = null);
		public Task<T> Add(T entity, params Expression<Func<T, object>>[] includes);
		public Task<T> Add(T entity);

		public Task<bool> Delete(T entity);
		public Task<T> Update(T entity);
		//public Task<T> AddOrUpdateAsync(T entity);
	}
}

