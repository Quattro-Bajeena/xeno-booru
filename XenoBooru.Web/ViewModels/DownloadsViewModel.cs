using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.ViewModels
{
	public class DownloadsViewModel
	{
		public Dictionary<string, string> Urls { get; set; }
		public Dictionary<string, int> Downloads { get; set; }
	}
}
