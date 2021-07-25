using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Services;

namespace XenoBooru.Web.Controllers
{
	public class CommentController : Controller
	{

		private readonly CommentService _comments;
		public CommentController(CommentService comments)
		{
			_comments = comments;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}

		[HttpPost]
		public IActionResult Add(Comment comment)
		{
			if (ModelState.IsValid && comment.Author != null && comment.Content != null)
			{
				_comments.Add(comment);
			}

			return RedirectToAction("Show", "Post", new { id = comment.PostId});
		}
	}
}
