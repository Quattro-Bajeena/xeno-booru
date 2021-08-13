using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Data.Repositories
{
	public class SQLTagRepository : ITagRepository
	{
		private readonly AppDbContext _context;
		public SQLTagRepository(AppDbContext context)
		{
			_context = context;
		}

		public TagEntity Get(string name)
		{
			return _context.Tags.Where(tag => tag.Name == name).FirstOrDefault();
		}

		public IEnumerable<TagEntity> GetAll()
		{
			return _context.Tags;
		}

		public IEnumerable<TagEntity> GetFromPost(int postId)
		{
			var postDb = _context.Posts.Include(post => post.Tags).SingleOrDefault(post => post.Id == postId);
			return postDb.Tags;
		}

		public void Add(TagEntity tag)
		{
			_context.Add(tag);
			_context.SaveChanges();
		}

		public void Remove(int id)
		{
			var tag = _context.Tags.Find(id);
			_context.Remove(tag);
			_context.SaveChanges();
		}

		public void Update(TagEntity tag)
		{
			_context.Update(tag);
			_context.SaveChanges();
		}

		public int GetTagPostCount(int tagId)
		{
			var posts = _context.Tags.Include(tag => tag.Posts).Single(tag => tag.Id == tagId).Posts;
			return posts.Count;
		}

		public ICollection<TagEntity> GetFromStr(string tagsStr)
		{
			if(tagsStr == null)
			{
				return new List<TagEntity>();
			}
			var tagsStrLst = tagsStr.Split(' ');
			return _context.Tags.Where(tag => tagsStrLst.Contains(tag.Name)).ToList();
		}

		 
	}
}
