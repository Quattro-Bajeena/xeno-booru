using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

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

		public IEnumerable<PostEntity> GetAll()
		{
			return _context.Posts;
		}

		public void Add(PostEntity post)
		{
			_context.Posts.Add(post);
			_context.SaveChanges();
		}

		public void Remove(int id)
		{
			var post = _context.Posts.Find(id);
			_context.Posts.Remove(post);
		}

		public void Update(PostEntity post)
		{
			_context.Posts.Update(post);
			_context.SaveChanges();
		}

		public IEnumerable<PostEntity> GetByTags(ICollection<string> tags)
		{
			var posts = _context.Posts
				.Include(post => post.Tags)
				.Where(post => post.Tags.Where(tag => tags.Contains(tag.Name)).Count() == tags.Count)
				.ToList();
				
			return posts;

		}
	}
}
