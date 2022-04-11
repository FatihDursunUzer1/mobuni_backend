using System;
namespace MobUni.ApplicationCore.Interfaces
{
	public interface IRepository<T>
	{
		public Task<T> GetById();
		public List<T> GetAll();
		public Task<T> Add(T entity);
		public Task<bool> Delete(T entity);
		public Task<T> Update(T entity);
	}
}

