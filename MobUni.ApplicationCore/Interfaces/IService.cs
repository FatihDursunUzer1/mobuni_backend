using MobUni.ApplicationCore.Result.Abstract;
using System;
namespace MobUni.ApplicationCore.Interfaces
{
	public interface IService<TDTO,CreateDTO>
	{
		public IDataResult<TDTO> GetById(int id);
		public Task<bool> Delete(TDTO dto);
		public Task<IDataResult<TDTO>> Add(CreateDTO dto,string? userId=null);
		public Task<IDataResult<TDTO>> Update(TDTO dto);
		public Task<IDataResult<List<TDTO>>> GetAll();
	}
}

