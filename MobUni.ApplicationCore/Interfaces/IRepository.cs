using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace MobUni.ApplicationCore.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public T GetById(int id);
		List<T> GetAll(Expression<Func<T, bool>> exp = null, int? PageIndex = null, int? PageSize = null);
		public Task<T> Add(T entity, params Expression<Func<T, object>>[] includes);
		public Task<T> Add(T entity);

		public Task<bool> Delete(T entity);
		public Task<T> Update(T entity, int entityId);
		//public Task<T> AddOrUpdateAsync(T entity);
	}
}

