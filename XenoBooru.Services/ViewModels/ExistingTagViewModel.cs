using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Services.ViewModels
{
	public class ExistingTagViewModel
	{
		public string Type { get; set; }
		public string Name { get; set; }
		public int PostCount { get; set; }
	}
}
