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
		private readonly AuthenticationService _authentication;
		public PoolController(PoolService pools, IOptions<AppConfig> config,
			AuthenticationService authentication)
		{
			_pools = pools;
			_config = config;
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

			ViewData["ContainerUrl"] = _config.Value.StorageUrl + "/" + _config.Value.StorageContainer;
			ViewData["AudioThumbnailFileName"] = _config.Value.AudioThumbnailFileName;

			return View(pool);
		}

		[HttpPost]
		public IActionResult AddPool(Pool pool)
		{
			return BadRequest();
		}

		[HttpPost]
		public IActionResult AddPoolEntry(int id, int postId)
		{
			if (_authentication.CheckAuthentication() == false)
			{
				return RedirectToAction("Show", new { id });
			}


			_pools.AddPoolEntry(id, postId);
			return RedirectToAction("Show", new { id });
		}
	}
}
