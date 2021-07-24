using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PostService
	{
		private readonly IPostRepository _postRepository;
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;

		public PostService(IPostRepository postRepository, ITagRepository tagRepository, IMapper mapper)
		{
			_postRepository = postRepository;
			_tagRepository = tagRepository;
			_mapper = mapper;
		}

		public Post Get(int id)
		{
			var dbPost = _postRepository.Get(id);
			var post = _mapper.Map<Post>(dbPost);
			return post;
		}

		public IEnumerable<Post> GetAll()
		{
			var dbPosts = _postRepository.GetAll();
			var posts = _mapper.Map<IEnumerable<Post>>(dbPosts);
			return posts;
		}

		public IEnumerable<Post> GetFiltered(ICollection<string> tagsStr)
		{
			var postsDb = _postRepository.GetByTags(tagsStr);
			var posts = _mapper.Map<IEnumerable<Post>>(postsDb);

			return posts;
		}

	}
}
