using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.DTO;
using XenoBooru.Services;
using XenoBooru.Web.Models;
using XenoBooru.Web.Services;

namespace XenoBooru.Web.Controllers
{
	public class PostController : Controller
	{
		private readonly PostService _posts;
		private readonly IOptions<AppConfig> _config;

		public PostController(PostService posts, IOptions<AppConfig> config)
		{
			_posts = posts;
			_config = config;
		}

		public IActionResult Index()
		{
			IEnumerable<Post> postList = _posts.GetAllPosts();
			return View(postList);
		}


		public IActionResult Show(int? id)
		{

			if (id == null || id <= 0)
			{
				return NotFound();
			}

			Post post = _posts.GetPost((int)id);
			ViewData["PostUrl"] = $"{_config.Value.MapStorageUrl}/{post.FileName}";

			return View(post);

		}
	}
}
