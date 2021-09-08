﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;
using XenoBooru.Core.Models;

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
			return _context.Tags.FirstOrDefault(tag => tag.Name == name);
		}

		public IEnumerable<TagEntity> GetAll()
		{
			return _context.Tags;
		}

		private IQueryable<TagEntity> Sort(IQueryable<TagEntity> tags, TagOrder order)
        {
            switch (order)
            {
                case TagOrder.Name:
					return tags.OrderBy(tag => tag.Name);
                case TagOrder.Count:
					return tags.OrderByDescending(tag => tag.Posts.Count);
                default:
					return tags;
            }
        }

		private IQueryable<TagEntity> Filter(IQueryable<TagEntity> tags, string nameQuery, string type)
        {
			if(String.IsNullOrEmpty(nameQuery) == false)
            {
				tags = tags.Where(tag => EF.Functions.Like( tag.Name, $"%{nameQuery}%"));
			}
			if(type != null)
            {
				tags = tags.Where(tag => tag.Type == type);
			}
			return tags;
        }

		public IEnumerable<TagEntity> GetFilteredSortedPaged(string name, string type, TagOrder order, int page, int onPage)
		{
			var tagsAll = _context.Tags.Include(tag => tag.Posts).ToList();
			var tags = _context.Tags.Include(tag => tag.Posts);
			var filtered = Filter(tags, name, type);
			var sorted = Sort(filtered, order);
			var paged = sorted.Skip((page - 1) * onPage).Take(onPage);
			// todo look up sql query that this emits
			var sql = paged.ToQueryString();
			var listed = paged.ToList();

			return paged;
		}

		public IEnumerable<TagEntity> GetFromPost(int postId)
		{
			var postDb = _context.Posts.Include(post => post.Tags).FirstOrDefault(post => post.Id == postId);
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
			var posts = _context.Tags.Include(tag => tag.Posts).FirstOrDefault(tag => tag.Id == tagId).Posts;
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

		public int Count()
        {
			return _context.Tags.Count();
        }



	}
}
