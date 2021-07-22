using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.DTO;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PostService
	{
		private readonly IPostRepository _repository;
		public PostService(IPostRepository repository)
		{
			_repository = repository;
		}

		public Post GetPost(int id)
		{
			var dbPost = _repository.GetPost(id);
			return new Post{ Id = 1, FileName = "level1.glb" };
		}

		public IEnumerable<Post> GetAllPosts()
		{
			var dbPosts = _repository.GetAllPosts();
			return new List<Post> { new Post { Id = 1 }, new Post { Id = 2 } };
		}


	}
}
