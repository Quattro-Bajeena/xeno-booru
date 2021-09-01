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
using XenoBooru.Web.Helpers;
using XenoBooru.Core.Configuration;

namespace XenoBooru.Web.Controllers
{
	public class PostController : Controller
	{
		private readonly PostService _posts;
		private readonly TagService _tags;
		private readonly CommentService _comments;
		private readonly PoolService _pools;
		private readonly AppOptions _options;
		private readonly AuthenticationService _authentication;

		public PostController(PostService posts, TagService tags, CommentService comments, PoolService pools, IOptions<AppOptions> options,
			AuthenticationService authentication)
		{
			_posts = posts;
			_tags = tags;
			_comments = comments;
			_pools = pools;

			_options = options.Value;
			_authentication = authentication;
		}

		public IActionResult Index(string tags, string showPending, int page = 1, int onPage = 50)
		{

			bool incldePending = showPending == "on";
			bool includeChildren = tags != null;

			// get accurete count
			int postCount = _posts.Count(tags, incldePending, includeChildren);
			onPage = onPage == -1 ? postCount : onPage;
			var posts = _posts.GetByTagsPaged(tags, page, onPage, incldePending, includeChildren);


			int pageCount = onPage > 0 ? (int)Math.Ceiling((double)postCount / onPage) : 0;
			IEnumerable<object> pages = WebHelpers.Pages(page, pageCount);

			var tagsOnPage = _tags.GetFromPosts(posts);

			var viewModel = new PostSearchViewModel
			{
				Posts = posts,
				Tags = tagsOnPage,
				SearchedTags = tags,
				ContainerUrl = _options.PostsContainerUrl,
				AudioThumbnailFileName = _options.AudioThumbnailFileName,
				CurrentPage = page,
				PageCount = pageCount,
				OnPage = onPage,
				ShowPending = showPending,
				Pages = pages
			};
			return View(viewModel);
		}

		public IActionResult Ranking(int page = 1, int onPage = 100)
		{
			int postCount = _posts.Count(null, false, true);
			var posts = _posts.GetMostLikedPaged( page, onPage);
			int pageCount = onPage > 0 ? (int)Math.Ceiling((double)postCount / onPage) : 0;
			IEnumerable<object> pages = WebHelpers.Pages(page, pageCount);

			var viewModel = new PostRankingViewModel
			{
				Posts = posts,
				CurrentPage = page,
				OnPage = onPage,
				PageCount = pageCount,
				Pages = pages
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
				DataUrl = _options.PostsContainerUrl + "/" + post.FileName,
				DownloadUrl = post.DownloadUrl(_options.PostsContainerUrl),
				Liked = _posts.UserLiked(post.Id, _authentication.GetIp())
			};


			return View(viewModel);

		}

		public IActionResult Upload()
		{
			if (_authentication.CheckAuthentication("UploadPost") == false)
			{
				return RedirectToAction("Index");
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
				return RedirectToAction("Show", new { id });

			_posts.Remove(id);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult GiveLike(int id)
		{
			_posts.GiveLike(id, _authentication.GetIp());
			return RedirectToAction("Show", new { id });
		}
	}
}
