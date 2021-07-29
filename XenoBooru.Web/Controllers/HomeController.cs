﻿using Azure.Storage.Blobs;
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
		private readonly IOptions<AppConfig> _config;
		private readonly AuthenticationService _authentication;

		public HomeController(ILogger<HomeController> logger, IOptions<AppConfig> config, AuthenticationService authentication)
		{
			_logger = logger;
			_config = config;
			_authentication = authentication;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login(bool? authorized)
		{
			ViewData["authorized"] = authorized;
			return View();
		}

		[HttpPost]
		public IActionResult Authenticate(string password)
		{
			//bool authorized = false;
			//if (_config.Value.Passwords.Contains(password))
			//{
			//	HttpContext.Session.SetInt32("authenticated", 1);
			//	authorized = true;
			//}
			//else
			//{
			//	HttpContext.Session.SetInt32("authenticated", 0);
			//}
			bool authorized = _authentication.Authenticate(password);
			
			return RedirectToAction("Login", new { authorized });
		}


		public IActionResult Testing()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
