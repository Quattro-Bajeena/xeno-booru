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
using XenoBooru.Data.Repositories;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PostService
	{
		private readonly IPostRepository _postRepository;
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;
		private readonly AzurePostClient _azurePostClient;

		public PostService(IPostRepository postRepository, ITagRepository tagRepository, IMapper mapper, AzurePostClient azurePostClient)
		{
			_postRepository = postRepository;
			_tagRepository = tagRepository;
			_mapper = mapper;
			_azurePostClient = azurePostClient;
		}

		public Post Get(int id)
		{
			var dbPost = _postRepository.Get(id);
			var post = _mapper.Map<Post>(dbPost);
			return post;
		}

		public ICollection<Post> GetByTagsPaged(string tags, int page, int onPage, bool includePending, bool includeChildren)
		{
			if(onPage == 0)
			{
				return new List<Post>();
			}
			string[] tagsArr;
			if (tags != null)
			{
				tagsArr = tags.Trim().Split(' ');
			}
			else
			{
				tagsArr = Array.Empty<string>();
			}

			var postsDb = _postRepository.GetByTagsPaged(tagsArr, includePending, includeChildren, page, onPage);
			var posts = _mapper.Map<ICollection<Post>>(postsDb);

			return posts;
		}

		public ICollection<Post> GetMostLikedPaged(int page, int onPage)
		{
			var postsDb = _postRepository.GetMostLikedPaged(page, onPage);
			var posts = _mapper.Map<ICollection<Post>>(postsDb);
			return posts;
		}

		public int Count(string tags, bool includePending, bool includeChildren)
		{
			string[] tagsArr;
			if (tags != null)
			{
				tagsArr = tags.Trim().Split(' ');
			}
			else
			{
				tagsArr = Array.Empty<string>();
			}
			return _postRepository.Count(tagsArr, includePending, includeChildren);
		}

		public int Add(Post post, string tagsStr, Stream fileStream)
		{

			var filename = Path.GetFileNameWithoutExtension(post.FileName);
			var extension = Path.GetExtension(post.FileName);

			post.FileName = $"{filename}_{Guid.NewGuid()}{extension}";

			_azurePostClient.Upload(post.FileName, fileStream);

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

		public void GiveLike(int id, string ip_adress)
		{
			_postRepository.GiveLike(id, ip_adress);
		}

		public bool UserLiked(int id, string ip_adress)
		{
			return _postRepository.UserLiked(id, ip_adress);
		}

		public void AddChild(Post parent, Post child)
		{
			var parentDb = _mapper.Map<PostEntity>(parent);
			var childDb = _mapper.Map<PostEntity>(child);
			_postRepository.AddChild(parentDb, childDb);
		}

		public ICollection<Post> GetChildren(int id)
		{
			var childrenDb = _postRepository.GetChildren(id);
			var children = _mapper.Map<ICollection<Post>>(childrenDb);
			return children;
		}

	}
}
