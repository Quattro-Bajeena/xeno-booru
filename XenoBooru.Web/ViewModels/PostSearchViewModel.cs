using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
	public class PostSearchViewModel : PagedViewModel
	{
		public string SearchedTags { get; set; }
		public string ShowPending { get; set; }
		public ICollection<Post> Posts { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public string ContainerUrl { get; set; }
		public string AudioThumbnailFileName { get; set; }

		public Dictionary<string, string> PagingRouteData => new Dictionary<string, string>
		{
			{ "tags", SearchedTags },
			{ "showPending", ShowPending },
			{ "onPage", OnPage.ToString() }
			//{"page", CurrentPage.ToString() }
		};
	}
}
