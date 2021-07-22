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

		

		public PostEntity GetPost(int id)
		{
			return _context.Posts.Find(id);
		}

		public IEnumerable<PostEntity> GetAllPosts()
		{
			return _context.Posts;
		}
	}
}
