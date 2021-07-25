using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			return _context.Pools.Find(id);
		}

		public IEnumerable<PoolEntity> GetAll()
		{
			return _context.Pools;
		}

		public int Add(PoolEntity pool)
		{
			_context.Pools.Add(pool);
			_context.SaveChanges();
			return pool.Id;
		}

		public void AddPost(int id, PostEntity post)
		{
			var pool = _context.Pools.Where(pool => pool.Id == id).Include(pool => pool.Posts).Single();
			pool.Posts.Add(post);
			_context.SaveChanges();
		}
		public void RemovePost(int id, int postId)
		{
			var pool = _context.Pools.Where(pool => pool.Id == id).Include(pool => pool.Posts).Single();
			var post = _context.Posts.Find(postId);
			pool.Posts.Remove(post);
			_context.SaveChanges();
		}


		public IEnumerable<PoolEntity> GetByPost(int postId)
		{
			var post = _context.Posts.Where(post => post.Id == postId).Include(post => post.Pools).Single();
			return post.Pools;
		}

		
	}
}
