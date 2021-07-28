using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Utilities;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Data.Repositories
{
	public class SQLPoolRepository : IPoolRepository
	{
		private readonly AppDbContext _context;
		public SQLPoolRepository(AppDbContext context)
		{
			_context = context;
		}

		public PoolEntity Get(int id)
		{
			return _context.Pools
				.Include(pool => pool.Entires)
				.Where(pool => pool.Id == id)
				.FirstOrDefault();
		}

		public PoolEntity GetFull(int id)
		{
			return _context.Pools
				.Include(pool => pool.Entires)
				.ThenInclude(entry => entry.Post)
				.Where(pool => pool.Id == id)
				.FirstOrDefault();
		}

		public IEnumerable<PoolEntity> GetAll()
		{
			return _context.Pools.Include(pool => pool.Entires);
		}

		public IEnumerable<PoolEntity> GetFiltered(string query)
		{
			var pool = _context.Pools
				.Include(pool => pool.Entires)
				.ToList()
				.Where(pool => pool.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
			return pool;
		}

		public int Add(PoolEntity pool)
		{
			_context.Pools.Add(pool);
			_context.SaveChanges();
			return pool.Id;
		}

		public void AddPost(int id, PostEntity post)
		{
			var pool = _context.Pools.Where(pool => pool.Id == id).Include(pool => pool.Entires).Single();

			var newEntry = new PoolEntryEntity
			{
				Post = post,
				Pool = pool,
				Position = pool.Entires.Count + 1
			};

			pool.Entires.Add(newEntry);
			_context.SaveChanges();
		}
		public void RemovePost(int id, int postId)
		{
			var pool = _context.Pools.Where(pool => pool.Id == id).Include(pool => pool.Entires).Single();
			var entry = _context.PoolEntries.Where(entry => entry.PoolId == id).Where(entry => entry.PostId == postId).Single();

			pool.Entires.Remove(entry);

			_context.SaveChanges();
			
		}


		public IEnumerable<PoolEntryEntity> GetByPost(int postId)
		{
			var post = _context.Posts
				.Where(post => post.Id == postId)
				.Include(post => post.PoolsEntries)
				.ThenInclude(entry => entry.Pool)
				.Single();

			return post.PoolsEntries;

		}

		public (int?, int?) AdjacmentPostId(PoolEntryEntity entry)
		{
			
			var previousPost = _context.PoolEntries
				.Where(e => e.PoolId == entry.PoolId)
				.Where(e => e.Position == entry.Position - 1)
				.FirstOrDefault();
			

			var nextPost = _context.PoolEntries
					.Where(e => e.PoolId == entry.PoolId)
					.Where(e => e.Position == entry.Position + 1)
					.FirstOrDefault();

			int? previous = null;
			int? next = null;

			if (previousPost != null)
				previous = previousPost.PostId;

			if (nextPost != null)
				next = nextPost.PostId;

			return (previous, next);
			
		}

		public IEnumerable<PoolEntryEntity> GetPoolEntries(int poolId)
		{
			var entries = _context.PoolEntries
				.Include(entry => entry.Post)
				.Where(entry => entry.PoolId == poolId);
			return entries;
		}
	}
}
