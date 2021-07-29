using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Services;
using XenoBooru.Web.Services;

namespace XenoBooru.Web.Controllers
{
	public class PoolController : Controller
	{
		private readonly PoolService _pools;
		private readonly IOptions<AppConfig> _config;
		public PoolController(PoolService pools, IOptions<AppConfig> config)
		{
			_pools = pools;
			_config = config;
		}


		public IActionResult Index(string query)
		{
			var pools = _pools.GetFiltered(query);
			return View(pools);
		}


		public IActionResult Show(int id)
		{
			var pool = _pools.Get(id);

			ViewData["ContainerUrl"] = _config.Value.StorageUrl + "/" + _config.Value.StorageContainer;
			ViewData["AudioThumbnailFileName"] = _config.Value.AudioThumbnailFileName;

			return View(pool);
		}

		[HttpPost]
		public IActionResult AddPool(Pool pool)
		{
			return null;
		}

		[HttpPost]
		public IActionResult AddPoolEntry(int id, int postId)
		{
			if (Convert.ToBoolean(HttpContext.Session.GetInt32("authenticated") ?? 0) == false)
			{
				TempData["AuthFailure"] = true;
				return RedirectToAction("Show", new { id });
			}
			_pools.AddPoolEntry(id, postId);
			return RedirectToAction("Show", new { id });
		}
	}
}
