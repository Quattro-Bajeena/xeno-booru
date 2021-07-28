using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Services;

namespace XenoBooru.Web.Controllers
{
	public class PoolController : Controller
	{
		private readonly PoolService _pools;
		public PoolController(PoolService pools)
		{
			_pools = pools;
		}


		public IActionResult Index(string query)
		{
			var pools = _pools.GetFiltered(query);
			return View(pools);
		}


		public IActionResult Show(int id)
		{
			var pool = _pools.Get(id);
			return View(pool);
		}

		[HttpPost]
		public IActionResult AddPool(Pool pool)
		{
			return null;
		}

		[HttpPost]
		public IActionResult AddPoolEntry(PoolEntry entry)
		{
			return null;
		}
	}
}
