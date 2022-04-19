using System;
namespace MobUni.ApplicationCore.Interfaces
{
	public interface IService<TDTO,CreateDTO>
	{
		public TDTO GetById(int id);
		public Task<bool> Delete(TDTO dto);
		public Task<TDTO> Add(CreateDTO dto);
		public Task<TDTO> Update(TDTO dto);
		public Task<List<TDTO>> GetAll();
	}
}

