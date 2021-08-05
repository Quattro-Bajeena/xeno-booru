using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PoolService
	{
		private readonly IPostRepository _postRepository;
		private readonly IPoolRepository _poolRepository;
		private readonly IMapper _mapper;

		public PoolService(IPostRepository postRepository, IPoolRepository poolRepository, IMapper mapper)
		{
			_postRepository = postRepository;
			_poolRepository = poolRepository;
			_mapper = mapper;
		}

		public Pool Get(int id)
		{
			var poolDb = _poolRepository.GetFull(id);
			var pool = _mapper.Map<Pool>(poolDb);
			return pool;
		}

		public IEnumerable<Pool> GetFiltered(string query)
		{
			query ??= "";
			var poolsDb = _poolRepository.GetFiltered(query);
			var pools = _mapper.Map <IEnumerable<Pool>>(poolsDb);
			return pools;
		}

		public IEnumerable<PoolEntry> GetPostEntries(int postId)
		{
			var entriesDb = _poolRepository.GetByPost(postId);

			IEnumerable<PoolEntry> entries = entriesDb.Select(entryDb =>
			{
				var entry = _mapper.Map<PoolEntry>(entryDb);
				var (previousPostId, nextPostId) = _poolRepository.AdjacmentPostId(entryDb);
				entry.PreviousPostId = previousPostId;
				entry.NextPostId = nextPostId;
				return entry;
			});

			return entries;
		}

		public IEnumerable<PoolEntry> GetPoolEntries(int poolId)
		{
			var entriesDb = _poolRepository.GetPoolEntries(poolId);
			var entries = _mapper.Map<IEnumerable<PoolEntry>>(entriesDb);
			return entries;
		}

		public void AddPoolEntry(int poolId, int postId)
		{
			_poolRepository.AddPoolEntry(poolId, postId);
		}

		public int AddPool(Pool pool)
        {
			var poolDb = _mapper.Map<PoolEntity>(pool);
			int newId = _poolRepository.Add(poolDb);
			return newId;
        }
	}
}
