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

		public IActionResult Index(string name, TagType? type, TagOrder order = TagOrder.Count, int page = 1)
		{
			const int onPage = 50;

			var tags = _tags.GetFilteredSortedPaged(name, type, order, page, onPage);
			int pageCount = (int)Math.Ceiling((double)_tags.Count() / onPage);

			var viewModel = new TagsViewModel
			{
				Tags = tags,
				Pages = WebHelpers.Pages(page, pageCount),
				CurrentPage = page,
				PageCount = pageCount,
				TagsOnPage = onPage,
				Type = type,
				Order = order
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
