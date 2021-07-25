using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface IPoolRepository
	{
		PoolEntity Get(int id);
		IEnumerable<PoolEntity> GetAll();
		int Add(PoolEntity pool);
		void AddPost(int id, PostEntity post);
		void RemovePost(int id, int postId);
		IEnumerable<PoolEntity> GetByPost(int postId);
		
	}
}
