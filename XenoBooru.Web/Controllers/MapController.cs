using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Web.Data;
using XenoBooru.Web.Models;
using XenoBooru.Web.Services;

namespace XenoBooru.Web.Controllers
{
	public class MapController : Controller
	{

		private readonly ApplicationDbContext _db;
		private readonly IOptions<AppConfig> _config;

		public MapController(ApplicationDbContext db, IOptions<AppConfig> config)
		{
			_db = db;
			_config = config;
		}

		public IActionResult Index()
		{
			IEnumerable<Map> mapList = _db.Maps;
			return View(mapList);
		}

		
		public IActionResult Show(int? id)
		{
			
			if(id == null || id == 0)
			{
				return NotFound();
			}

			Map map = _db.Maps.Find(id);
			ViewData["MapUrl"] = $"{_config.Value.MapStorageUrl}/level{id}.glb";

			return View(map);
			
		}
	}
}
