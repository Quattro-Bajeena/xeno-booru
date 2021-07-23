using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PostService
	{
		private readonly IPostRepository _repository;
		private readonly IMapper _mapper;

		public PostService(IPostRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public Post Get(int id)
		{
			var dbPost = _repository.Get(id);
			var post = _mapper.Map<Post>(dbPost);
			return post;
			//return new Post{ Id = 1, FileName = "level1.glb" };
		}

		public IEnumerable<Post> GetAll()
		{
			var dbPosts = _repository.GetAll();
			var posts = _mapper.Map<IEnumerable<Post>>(dbPosts);
			return posts;
		}


	}
}
