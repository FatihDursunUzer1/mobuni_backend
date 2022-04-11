using System;
namespace MobUni.ApplicationCore.Interfaces
{
	public interface IService<TDTO>
	{
		public Task<bool> Delete(TDTO dto);
		public Task<TDTO> Add(TDTO dto);
		public Task<TDTO> Update(TDTO dto);
		public List<TDTO> GetAll();
	}
}

