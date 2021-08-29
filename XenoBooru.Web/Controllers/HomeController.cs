using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Web.Services;
using XenoBooru.Web.ViewModels;

namespace XenoBooru.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppOptions _config;
		private readonly AuthenticationService _authentication;

		public HomeController(ILogger<HomeController> logger, IOptions<AppOptions> config, AuthenticationService authentication)
		{
			_logger = logger;
			_config = config.Value;
			_authentication = authentication;
		}

		public IActionResult Index() => View();

		public IActionResult Crash() => throw new Exception("oops, crashed");

		
		public IActionResult Login(bool authorized)
		{
			ViewData["authorized"] = authorized;
			return View();
		}

		
		public IActionResult About() => View();
		public IActionResult MapViewerApp()
		{
			ViewData["MapViewerDownloadUrl"] = _config.GetResourceUrl("XenogearsMapViewer.zip");
			return View();
		}

		public IActionResult Downloads()
		{
			ViewData["MapViewerDownloadUrl"] = _config.GetResourceUrl("XenogearsMapViewer.zip");
			ViewData["AllMapsDownloadUrl"] = _config.GetResourceUrl("XenogearsAllMaps.zip");
			return View();
		}

		[HttpPost]
		public IActionResult Authenticate(string password)
		{
			bool authorized = _authentication.Authenticate(password);
			return RedirectToAction("Login", new { authorized });
		}


		
	}
}
