using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Services;

namespace XenoBooru.Web.Controllers
{
	public class TagController : Controller
	{
		private readonly TagService _tags;
		public TagController(TagService tags)
		{
			_tags = tags;
		}

		public IActionResult Index()
		{
			var allTags = _tags.GetAll();
			return View(allTags);
		}

		public IActionResult GetExisting()
		{
			var tags = _tags.GetExisting();
			return Json(tags);
		}
	}
}
