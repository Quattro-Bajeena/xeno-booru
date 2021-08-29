using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Web.ViewModels;

namespace XenoBooru.Web.Controllers
{
	public class ErrorController : Controller
	{

		public IActionResult Handle(int code)
		{
			if(code == 404)
			{
				return View("PageNotFound");
			}

			return Content($"Error code: {code}");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Exception()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
