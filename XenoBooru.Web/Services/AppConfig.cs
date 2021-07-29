using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.Services
{
	public class AppConfig
	{
		public string StorageUrl { get; set; }
		public string StorageContainer { get; set; }
		public string AudioThumbnailFileName { get; set; }
		public List<string> Passwords { get; set; }
	}
}
