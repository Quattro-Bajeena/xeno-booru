using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
	public class PostSearchViewModel
	{
		public string SearchedTagsStr { get; set; }
		public ICollection<Post> Posts { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
	}
}
