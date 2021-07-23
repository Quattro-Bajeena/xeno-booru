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
		private readonly CommentService _comments;
		private readonly IOptions<AppConfig> _config;

		public PostController(PostService posts, CommentService comments, IOptions<AppConfig> config)
		{
			_posts = posts;
			_comments = comments;
			_config = config;

		}

		public IActionResult Index()
		{
			IEnumerable<Post> postList = _posts.GetAll();
			return View(postList);
		}


		public IActionResult Show(int? id)
		{
			Post post = null;
			if (id == null || id < 1 || (post = _posts.Get((int)id)) == null)
			{
				return NotFound();
			}
			IEnumerable<Comment> comments = _comments.GetFromPost((int)id);

			PostViewModel viewModel = new PostViewModel();
			viewModel.Post = post;
			viewModel.Comments = comments;
			viewModel.DataUrl = $"{_config.Value.StorageUrl}/{post.FileName}";
			viewModel.Tags = null;


			return View(viewModel);

		}
	}
}
