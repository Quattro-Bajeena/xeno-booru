using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.Services
{
	public class AppConfig
	{
		public string StorageUrl { get; set; }
		public string PostContainer { get; set; }
		public string ResourceContainer { get; set; }
		public string AudioThumbnailFileName { get; set; }
		public List<string> Passwords { get; set; }
        public Dictionary<string, bool> AuthenticationRequired { get; set; }
		public string PostsContainerUrl => StorageUrl + "/" + PostContainer;

		public string GetResourceUrl(string filename) => StorageUrl + "/" + ResourceContainer + "/" + filename;
	}
}
