using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Services;
using XenoBooru.Web.Helpers;
using XenoBooru.Web.ViewModels;

namespace XenoBooru.Web.Controllers
{
	public class TagController : Controller
	{
		private readonly TagService _tags;
		public TagController(TagService tags)
		{
			_tags = tags;
		}

		public IActionResult Index(string name, TagType type, string order,int page = 1, int onPage = 5)
		{
			var tags = _tags.GetAll();

			onPage = onPage == -1 ? tags.Count : onPage;
			var tagsDisplayed = tags.Skip((page - 1) * onPage).Take(onPage).ToList();
			int pageCount = (int)Math.Ceiling((double)tags.Count / onPage);

			var viewModel = new TagsViewModel
			{
				Tags = tagsDisplayed,
				Pages = WebHelpers.Pages(page, pageCount),
				CurrentPage = page,
				PageCount = pageCount,
				TagsOnPage = onPage
			};

			return View(viewModel);
		}


		public IActionResult GetExisting()
		{
			var tags = _tags.GetExisting();
			return Json(tags);
		}
	}
}
