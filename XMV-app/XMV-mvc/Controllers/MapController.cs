using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XMV_mvc.Data;

namespace XMV_mvc.Controllers
{
	public class MapController : Controller
	{

		private readonly ApplicationDbContext _db;
		public MapController(ApplicationDbContext db)
		{
			_db = db;
		}


		public IActionResult Index()
		{
			
			
			return View();
		}

		public IActionResult Display(int? id)
		{
			if (id == null)
			{
				return NotFound("There is no map");
			}
			else
			{
				Console.WriteLine("id isnt null");
			}
			Console.WriteLine(id);
			ViewData["id"] = id;
			return View();
		}
	}
}
