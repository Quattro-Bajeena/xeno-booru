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
using XenoBooru.Services;
using XenoBooru.Web.Services;
using XenoBooru.Web.ViewModels;

namespace XenoBooru.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppOptions _options;
		private readonly AuthenticationService _authentication;
		private readonly FileDownloadTrackingService _fileDownloads;

		public HomeController(
			ILogger<HomeController> logger, IOptions<AppOptions> config, 
			AuthenticationService authentication, FileDownloadTrackingService fileDownloads)
		{
			_logger = logger;
			_options = config.Value;
			_authentication = authentication;
			_fileDownloads = fileDownloads;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Crash() => throw new Exception("oops, crashed");

		
		public IActionResult Login(bool authorized)
		{
			ViewData["authorized"] = authorized;
			return View();
		}

		
		public IActionResult About() => View();
		public IActionResult MapViewerApp()
		{
			ViewData["MapViewerDownloadUrl"] = _options.GetResourceUrl("XenogearsMapViewer.zip");
			return View();
		}

		public IActionResult Downloads()
		{
			var fileUrls = new Dictionary<string, string>
			{
				{ "MapViewer", _options.GetResourceUrl("XenogearsMapViewerApp.zip") },
				{ "Maps",  _options.GetResourceUrl("XenogearsAllMaps.zip") },
				{ "SceneModels", _options.GetResourceUrl("SceneModels.zip") },
				{ "HeadsSlides", _options.GetResourceUrl("HeadsSlides.zip") }
			};

			var fileDownloads = new Dictionary<string, int>
			{
				{ "MapViewer", _fileDownloads.GetCount("MapViewer") },
				{ "Maps",  _fileDownloads.GetCount("AllMaps") },
				{ "SceneModels", _fileDownloads.GetCount("SceneModels") },
				{ "HeadsSlides", _fileDownloads.GetCount("HeadsSlides") }
			};


			var viewModel = new DownloadsViewModel
			{
				Urls = fileUrls,
				Downloads = fileDownloads
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Authenticate(string password)
		{
			bool authorized = _authentication.Authenticate(password);
			return RedirectToAction("Login", new { authorized });
		}

		[HttpPost]
		public JsonResult RegisterFileDownload(string name)
		{
			var succes = _fileDownloads.Register(name);
			var result = new
			{
				RegisteredDownload = succes
			};
			return new JsonResult(result);
		}


		
	}
}
