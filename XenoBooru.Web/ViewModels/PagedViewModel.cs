using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.ViewModels
{
	public class PagedViewModel
	{
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public int OnPage { get; set; }
		public IEnumerable<object> Pages { get; set; }
	}
}
