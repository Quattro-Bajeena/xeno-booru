using AutoMapper;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
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
		private readonly BlobContainerClient _blobContainer;

		public PostService(IPostRepository postRepository, ITagRepository tagRepository, IMapper mapper, BlobContainerClient blobContainer)
		{
			_postRepository = postRepository;
			_tagRepository = tagRepository;
			_mapper = mapper;
			_blobContainer = blobContainer;
		}

		public Post Get(int id)
		{
			var dbPost = _postRepository.Get(id);
			var post = _mapper.Map<Post>(dbPost);
			return post;
		}

		public IEnumerable<Post> GetFiltered(string tags)
		{
			string[] tagsArr;
			bool includePending;
			if (tags != null)
			{
				tagsArr = tags.Split(' ');
				includePending = false;
			}
			else
			{
				tagsArr = Array.Empty<string>();
				includePending = true;
			}

			var postsDb = _postRepository.GetByTags(tagsArr, includePending);
			var posts = _mapper.Map<IEnumerable<Post>>(postsDb);

			return posts;
		}


		public int Add(Post post, string tagsStr, Stream fileStream)
		{

			var filename = Path.GetFileNameWithoutExtension(post.FileName);
			var extension = Path.GetExtension(post.FileName);

			post.FileName = $"{filename}_{Guid.NewGuid()}{extension}";

			BlobClient blobClient = _blobContainer.GetBlobClient(post.FileName);
			blobClient.Upload(fileStream);

			var postDb = _mapper.Map<PostEntity>(post);

			postDb.Tags = _tagRepository.GetFromStr(tagsStr);

			int id = _postRepository.Add(postDb);
			return id;
		}

		public void Update(Post post, string tagsStr)
		{
			var updatedPostEntity = _mapper.Map<PostEntity>(post);
			updatedPostEntity.Tags = _tagRepository.GetFromStr(tagsStr);

			_postRepository.Update(updatedPostEntity);

		}

		public void Remove(int id)
		{
			_postRepository.Remove(id);
		}

	}
}
