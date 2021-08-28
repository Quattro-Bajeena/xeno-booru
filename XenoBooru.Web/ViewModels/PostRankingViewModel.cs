using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
	public class PostRankingViewModel : PagedViewModel
	{
		public IEnumerable<Post> Posts { get; set; }
	}
}
