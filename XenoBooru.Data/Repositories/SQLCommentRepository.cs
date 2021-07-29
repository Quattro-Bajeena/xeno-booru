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
	public class SQLCommentRepository : ICommentRepository
	{

		private readonly AppDbContext _context;
		public SQLCommentRepository(AppDbContext context)
		{
			_context = context;
		}

		

		public IEnumerable<CommentEntity> GetAll()
		{
			return _context.Comments.Include(comment => comment.Post).OrderByDescending(comment => comment.Date);
		}

		public IEnumerable<CommentEntity> GetFromPost(int postId)
		{
			return _context.Comments.Where(comment => comment.Post.Id == postId);
		}

		public void Add(CommentEntity comment)
		{
			_context.Comments.Add(comment);
			_context.SaveChanges();
		}

		public void Remove(int id)
		{
			var comment = _context.Comments.Find(id);
			_context.Comments.Remove(comment);
			_context.SaveChanges();
		}
	}
}
