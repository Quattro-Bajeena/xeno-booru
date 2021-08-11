using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Repositories.Interfaces;
using XenoBooru.Services.ViewModels;

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


		public ICollection<Tag> GetAll()
		{
			var tagsDb = _tagRepository.GetAll();
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			foreach (var tag in tags)
			{
				tag.PostCount = _tagRepository.GetTagPostCount(tag.Id);
			}
			var sortedTags = tags.OrderByDescending(tag => tag.PostCount).ToList();
			return sortedTags;
		}

		public ICollection<Tag> GetFiltered(string name, TagType type, TagOrder order)
        {
			
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

		public IEnumerable<ExistingTagViewModel> GetExisting()
		{
			var tagsDb = _tagRepository.GetAll();
			var tags = _mapper.Map<IEnumerable<ExistingTagViewModel>>(tagsDb);
			return tags;
		}

	}
}
