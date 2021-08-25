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

		private void AddTagCounts(IEnumerable<Tag> tags)
		{
			foreach (var tag in tags)
			{
				tag.PostCount = _tagRepository.GetTagPostCount(tag.Id);
			}
		}

		public ICollection<Tag> GetAll()
		{
			var tagsDb = _tagRepository.GetAll();
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			AddTagCounts(tags);
			var sortedTags = tags.OrderByDescending(tag => tag.PostCount).ToList();
			return sortedTags;
		}

		public ICollection<Tag> GetFilteredSortedPaged(string nameQuery, TagType? type, TagOrder order, int page, int onPage)
        {
			var prepName = nameQuery?.ToLower().Replace(' ', '_');
			var tagsDb = _tagRepository.GetFilteredSortedPaged(prepName, type?.ToString(),order, page, onPage).ToList();
			var tags = _mapper.Map<ICollection<Tag>>(tagsDb);
			foreach (var tag in tags)
			{
				tag.PostCount = tag.Posts.Count;
			}
			return tags;
        }

		public IEnumerable<Tag> GetFromPost(int postId)
		{
			var tagsDb = _tagRepository.GetFromPost(postId);
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			AddTagCounts(tags);
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
			return tags.Values.OrderBy(tag => tag.Type).ThenByDescending(tag => tag.PostCount).Take(limit);
		}

		public IEnumerable<ExistingTagViewModel> GetExisting()
		{
			var tagsDb = _tagRepository.GetAll();
			var tags = _mapper.Map<IEnumerable<Tag>>(tagsDb);
			AddTagCounts(tags);
			var tagsVm = _mapper.Map<IEnumerable<ExistingTagViewModel>>(tags);
			return tagsVm;
		}

		public int Count()
		{
			return _tagRepository.Count();
		}

	}
}
