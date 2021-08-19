using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
	public class PostSearchViewModel
	{
		public string SearchedTags { get; set; }
		public string ShowPending { get; set; }
		public ICollection<Post> Posts { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public string ContainerUrl { get; set; }
		public string AudioThumbnailFileName { get; set; }
		public IEnumerable<object> Pages { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public int PostsOnPage { get; set; }
		public Dictionary<string, string> PagingRouteData => new Dictionary<string, string>
		{
			{ "tags", SearchedTags },
			{ "showPending", ShowPending },
			{ "onPage", PostsOnPage.ToString() }
			//{"page", CurrentPage.ToString() }
		};
	}
}
