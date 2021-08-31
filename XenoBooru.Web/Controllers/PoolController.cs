using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Configuration;
using XenoBooru.Core.Models;
using XenoBooru.Services;
using XenoBooru.Web.Services;

namespace XenoBooru.Web.Controllers
{
	public class PoolController : Controller
	{
		private readonly PoolService _pools;
		private readonly IOptions<AppOptions> _options;
		private readonly AuthenticationService _authentication;
		public PoolController(PoolService pools, IOptions<AppOptions> options,
			AuthenticationService authentication)
		{
			_pools = pools;
			_options = options;
			_authentication = authentication;
		}


		public IActionResult Index(string query)
		{
			var pools = _pools.GetFiltered(query);
			return View(pools);
		}


		public IActionResult Show(int? id)
		{
			Pool pool;
			if (id == null || id < 1 || (pool = _pools.Get((int)id)) == null)
			{
				return NotFound();
			}

			ViewData["ContainerUrl"] = _options.Value.StorageUrl + "/" + _options.Value.PostContainer;
			ViewData["AudioThumbnailFileName"] = _options.Value.AudioThumbnailFileName;

			return View(pool);
		}

		public IActionResult Create()
        {
			if (_authentication.CheckAuthentication("CreatePool") == false)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		[HttpPost]
		public IActionResult Create(Pool pool)
		{
			
			var id = _pools.AddPool(pool);
			return RedirectToAction("Show", new { id });
		}

		[HttpPost]
		public IActionResult AddPoolEntry(int id, int postId)
		{
			if (_authentication.CheckAuthentication("AddPoolEntry") == true)
			{
				_pools.AddPoolEntry(id, postId);
			}
			return RedirectToAction("Show", new { id });
		}
	}
}
