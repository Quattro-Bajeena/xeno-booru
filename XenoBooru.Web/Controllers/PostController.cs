using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Services;
using XenoBooru.Web.ViewModels;
using XenoBooru.Web.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace XenoBooru.Web.Controllers
{
	public class PostController : Controller
	{
		private readonly PostService _posts;
		private readonly TagService _tags;
		private readonly CommentService _comments;
		private readonly PoolService _pools;
		private readonly IOptions<AppConfig> _config;
		private readonly AuthenticationService _authentication;

		public PostController(PostService posts, TagService tags, CommentService comments, PoolService pools, IOptions<AppConfig> config,
			AuthenticationService authentication)
		{
			_posts = posts;
			_tags = tags;
			_comments = comments;
			_pools = pools;

			_config = config;
			_authentication = authentication;
		}

		public IActionResult Index(string tags)
		{
			
			var posts = _posts.GetFiltered(tags);
			var tagsDisplayed = _tags.GetFromPosts(posts);

			var viewModel = new PostSearchViewModel
			{
				Posts = posts.ToList(),
				Tags = tagsDisplayed,
				SearchedTagsStr = tags,
				ContainerUrl = $"{_config.Value.StorageUrl}/{_config.Value.StorageContainer}",
				AudioThumbnailFileName = _config.Value.AudioThumbnailFileName
			};
			return View(viewModel);
		}


		public IActionResult Show(int? id)
		{
			Post post;
			if (id == null || id < 1 || (post = _posts.Get((int)id)) == null)
			{
				return NotFound();
			}



			var viewModel = new PostViewModel
			{
				Post = post,
				Comments = _comments.GetFromPost(post.Id),
				Tags = _tags.GetFromPost(post.Id).ToList(),
				PoolEntries = _pools.GetPostEntries(post.Id),
				DataUrl = $"{_config.Value.StorageUrl}/{_config.Value.StorageContainer}/{post.FileName}",
				Liked = _posts.UserLiked(post.Id, _authentication.GetIp())
			};


			return View(viewModel);

		}

		public IActionResult Upload()
		{
			if (_authentication.CheckAuthentication("UploadPost") == false)
			{
				return RedirectToAction("Upload");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Upload(Post post, string tags, IFormFile file)
		{
			if (ModelState.IsValid == false)
			{
				return RedirectToAction("Index");
			}

			post.FileName = file.FileName;
			int id = _posts.Add(post, tags, file.OpenReadStream());

			return RedirectToAction("Show", new { id });
		}

		[HttpPost]
		public IActionResult Update(int id, Post post, string tags)
		{
			if (_authentication.CheckAuthentication("UpdatePost") == false)
			{
				return RedirectToAction("Show", new { id });
			}

			post.Id = id;
			_posts.Update(post, tags);
			return RedirectToAction("Show", new { id });
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			if (_authentication.CheckAuthentication("DeletePost") == false)
			{
				return RedirectToAction("Show", new { id });
			}
			_posts.Remove(id);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult GiveLike(int id)
		{
			if(id < 1)
			{
				return NotFound();
			}

			_posts.GiveLike(id, _authentication.GetIp());
			return RedirectToAction("Show", new { id });
		}
	}
}
