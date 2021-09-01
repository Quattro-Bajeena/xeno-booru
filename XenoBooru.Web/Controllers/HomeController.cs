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
using XenoBooru.Core.Configuration;
using XenoBooru.Web.Services;
using XenoBooru.Web.ViewModels;

namespace XenoBooru.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppOptions _options;
		private readonly AuthenticationService _authentication;

		public HomeController(ILogger<HomeController> logger, IOptions<AppOptions> config, AuthenticationService authentication)
		{
			_logger = logger;
			_options = config.Value;
			_authentication = authentication;
		}

		public IActionResult Index()
		{
			_logger.LogInformation("Index");
			return View();
		}

		public IActionResult Crash() => throw new Exception("oops, crashed");

		
		public IActionResult Login(bool authorized)
		{
			_logger.LogInformation("Login");
			ViewData["authorized"] = authorized;
			return View();
		}

		
		public IActionResult About() => View();
		public IActionResult MapViewerApp()
		{
			_logger.LogError("MapViewerApp");
			ViewData["MapViewerDownloadUrl"] = _options.GetResourceUrl("XenogearsMapViewer.zip");
			return View();
		}

		public IActionResult Downloads()
		{
			_logger.LogWarning("Downloads");
			ViewData["MapViewerDownloadUrl"] = _options.GetResourceUrl("XenogearsMapViewer.zip");
			ViewData["AllMapsDownloadUrl"] = _options.GetResourceUrl("XenogearsAllMaps.zip");
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
