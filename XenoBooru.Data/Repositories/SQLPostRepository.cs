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
	public class SQLPostRepository : Interfaces.IPostRepository
	{
		private readonly AppDbContext _context;
		public SQLPostRepository(AppDbContext context)
		{
			_context = context;
		}

		

		public PostEntity Get(int id)
		{
			return _context.Posts.Find(id);
			//return _context.Posts.SingleOrDefault(post => post.Id == id);
			//return _context.Posts.Include(post => post.Tags).Include(post => post.Comments).Where(post => post.Id == id).First();
		}

		

		public int Add(PostEntity post)
		{
			_context.Posts.Add(post);
			_context.SaveChanges();
			return post.Id;
		}

		public void Remove(int id)
		{
			var post = _context.Posts.Include(post => post.Comments).Where(post => post.Id == id).FirstOrDefault();
			_context.Posts.Remove(post);
			_context.SaveChanges();
		}

		public void Update(PostEntity updatedPost)
		{
			var originalPost = _context.Posts.Where( post => post.Id == updatedPost.Id).Include(post => post.Tags).Single();

			originalPost.Name = updatedPost.Name;
			originalPost.Source = updatedPost.Source;
			originalPost.Credits = updatedPost.Credits;
			originalPost.Description = updatedPost.Description;
			originalPost.Tags = updatedPost.Tags;

			_context.Posts.Update(originalPost);
			_context.SaveChanges();
		}

		private IQueryable<PostEntity> GetAll(bool includePending)
		{

			if (includePending)
			{
				return _context.Posts;
			}
			else
			{
				return _context.Posts.Where(post => post.Pending == false);
			}
		}

		public IEnumerable<PostEntity> GetByTags(ICollection<string> tags, bool includePending)
		{
			var posts = GetAll(includePending)
				.Include(post => post.Tags)
				.Where(post => post.Tags.Where(tag => tags.Contains(tag.Name)).Count() == tags.Count)
				.ToList();
				
			return posts;

		}

		//public IList<PostEntity> GetFromPool(int poolId)
		//{
		//	var pool = _context.Pools.Where(pool => pool.Id == poolId).Include(pool => pool.Entires).Single();
		//	var entries = pool.Entires;
		//	var posts = entries.OrderBy(entry => entry.Position).Select(entry => entry.Post);

		//	return posts.ToList();
		//}

		public IEnumerable<PostEntity> GetFromPool(int poolId)
		{
			var entires = _context.PoolEntries.Where(entry => entry.PoolId == poolId);
			var posts = entires.Select(entry => entry.Post);
			return posts;
		}
	}
}
