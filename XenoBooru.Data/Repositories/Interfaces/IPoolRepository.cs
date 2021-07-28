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
		PoolEntity GetFull(int id);
		IEnumerable<PoolEntity> GetAll();
		IEnumerable<PoolEntity> GetFiltered(string query);
		int Add(PoolEntity pool);
		void AddPost(int id, PostEntity post);
		void RemovePost(int id, int postId);
		IEnumerable<PoolEntryEntity> GetByPost(int postId);
		(int?, int?) AdjacmentPostId(PoolEntryEntity entry);
		IEnumerable<PoolEntryEntity> GetPoolEntries(int poolId);
	}
}
