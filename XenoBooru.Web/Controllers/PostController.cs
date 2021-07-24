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
			if (tags != null)
				tagsArr = tags.Split(' ');
			else
				tagsArr = new string[0];

			var posts = _posts.GetFiltered(tagsArr);
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
			viewModel.Tags = _tags.GetFromPost(post.Id);
			viewModel.DataUrl = $"{_config.Value.StorageUrl}/{post.FileName}";

			return View(viewModel);

		}
	}
}
