using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Data.Repositories
{

	public class SQLPostRepository : IPostRepository
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

		public ICollection<PostEntity> GetByTagsPaged(ICollection<string> tags, bool includePending, bool includeChildren, int page, int onPage)
		{
			var posts = _context.Posts
				.ByPending(includePending)
				.IncludeChildren(includeChildren)
				.Include(post => post.Tags).Include(post => post.Parent)
				.Where(post => post.Tags.Where(tag => tags.Contains(tag.Name)).Count() == tags.Count)
				.Skip( (page - 1) * onPage)
				.Take(onPage)
				.ToList();

			return posts;
		}

		public int Count(ICollection<string> tags, bool includePending, bool includeChildren)
		{
			return _context.Posts
				.ByPending(includePending)
				.IncludeChildren(includeChildren)
				.Where(post => post.Tags.Where(tag => tags.Contains(tag.Name)).Count() == tags.Count)
				.Count();
		}


		public IEnumerable<PostEntity> GetFromPool(int poolId)
		{
			var entires = _context.PoolEntries.Where(entry => entry.PoolId == poolId);
			var posts = entires.Select(entry => entry.Post);
			return posts;
		}

		public void GiveLike(int id, string ip_adress)
		{
			var post = _context.Posts.Where(post => post.Id == id).FirstOrDefault();
			var user_like = _context.UserLikes
				.Where(like => like.PostId == id)
				.Where(like => like.IpAdress == ip_adress)
				.FirstOrDefault();

			if (post != null && user_like == null)
			{
				user_like = new UserLikeEntity
				{
					PostId = id,
					IpAdress = ip_adress,
					Post = post
				};

				_context.UserLikes.Add(user_like);
				post.Likes += 1;
				_context.SaveChanges();

			}
		}

		public bool UserLiked(int id, string ip_adress)
		{
			var user_like = _context.UserLikes
				.Where(like => like.PostId == id)
				.Where(like => like.IpAdress == ip_adress)
				.FirstOrDefault();
			return user_like != null;
		}

		public void AddChild(PostEntity parent, PostEntity child)
		{
			if(child.ParentId == null)
			{
				child.Parent = parent;
			}
			
			_context.SaveChanges();
		}

		public ICollection<PostEntity> GetChildren(int id)
		{
			return _context.Posts.Where(post => post.ParentId == id).ToList();
		}
	}
}
