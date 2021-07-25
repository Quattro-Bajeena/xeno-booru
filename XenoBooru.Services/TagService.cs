﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class TagService
	{
		private readonly ITagRepository _tagRepository;
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;

		public TagService(ITagRepository tagRepository, IPostRepository postRepository, IMapper mapper)
		{
			_tagRepository = tagRepository;
			_postRepository = postRepository;
			_mapper = mapper;
		}


		public IEnumerable<Tag> GetAll()
		{
			var tagsDb = _tagRepository.GetAll();
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			foreach (var tag in tags)
			{
				tag.PostCount = _tagRepository.GetTagPostCount(tag.Id);
			}
			tags = tags.OrderByDescending(tag => tag.PostCount);
			return tags;
		}

		public IEnumerable<Tag> GetFromPost(int postId)
		{
			var tagsDb = _tagRepository.GetFromPost(postId);
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			foreach (var tag in tags)
			{
				tag.PostCount = _tagRepository.GetTagPostCount(tag.Id);
			}
			tags = tags.OrderByDescending(tag => tag.PostCount);
			return tags;
		}

		public IEnumerable<Tag> GetFromPosts(IEnumerable<Post> posts, int limit = 20)
		{
			var tags = new Dictionary<int, Tag>();

			foreach (var post in posts)
			{
				var postTagsDb = _tagRepository.GetFromPost(post.Id);
				var postTags = _mapper.Map<IEnumerable<Tag>>(postTagsDb);
				foreach (var tag in postTags)
				{
					if (tags.ContainsKey(tag.Id))
					{
						tags[tag.Id].PostCount += 1;
					}
					else
					{
						tags[tag.Id] = tag;
						tags[tag.Id].PostCount = 1;
					}
				}
			}
			return tags.Values.OrderByDescending(tag => tag.PostCount).Take(limit);
		}

		public ICollection<Tag> GetFromString(string tagsStr)
		{
			var tags = new List<Tag>();
			var tagsStrLst = tagsStr.Split(' ');

			foreach (var tagStr in tagsStrLst)
			{
				var tagDb = _tagRepository.Get(tagStr);
				var tag = _mapper.Map<Tag>(tagDb);
				tags.Add(tag);
			}
			return tags;
		}
	}
}
