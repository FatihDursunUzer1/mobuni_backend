using Microsoft.EntityFrameworkCore;
using System;
namespace MobUni.ApplicationCore.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public T GetById(int id);
		public Task<List<T>> GetAll();
		public Task<T> Add(T entity);
		public Task<bool> Delete(T entity);
		public Task<T> Update(T entity,int entityId);
	}
}

