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

namespace XenoBooru.Web.Controllers
{
	public class PostController : Controller
	{
		private readonly PostService _posts;
		private readonly TagService _tags;
		private readonly CommentService _comments;
		private readonly IOptions<AppConfig> _config;

		public PostController(PostService posts, TagService tags, CommentService comments,  IOptions<AppConfig> config)
		{
			_posts = posts;
			_tags = tags;
			_comments = comments;
			_config = config;

		}

		public IActionResult Index(string tags)
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
				tagsArr = new string[0];
				includePending = true;
			}
				
			var posts = _posts.GetFiltered(tagsArr, includePending);
			var tagsDisplayed = _tags.GetFromPosts(posts);

			var viewModel = new PostSearchViewModel();
			viewModel.Posts = posts.ToList();
			viewModel.Tags = tagsDisplayed;
			viewModel.SearchedTagsStr = tags;

			return View(viewModel);
		}


		public IActionResult Show(int? id)
		{
			Post post = null;
			if (id == null || id < 1 || (post = _posts.Get((int)id)) == null)
			{
				return NotFound();
			}


			PostViewModel viewModel = new PostViewModel();
			viewModel.Post = post;
			viewModel.Comments = _comments.GetFromPost(post.Id);
			viewModel.Tags = _tags.GetFromPost(post.Id).ToList();
			viewModel.DataUrl = $"{_config.Value.StorageUrl}/{post.FileName}";


			return View(viewModel);

		}

		public IActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Upload(Post post, string tags)
		{
			if (ModelState.IsValid == false)
			{
				return RedirectToAction("Index");
			}

			//var tagsLst = _tags.GetFromString(tags);
			int id = _posts.Add(post, tags);

			return RedirectToAction("Show", new {id = id });
		}

		[HttpPost]
		public IActionResult Update(int id, Post post, string tags)
		{
			post.Id = id;
			_posts.Update(post, tags);
			return RedirectToAction("Show", new { id = post.Id });
		}

		
		public IActionResult Delete(int id)
		{
			_posts.Remove(id);
			return RedirectToAction("Show");
		}
	}
}
