using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
	public class PostViewModel
	{
		public string DataUrl { get; set; }
		public Post Post { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public IEnumerable<PoolEntry> PoolEntries { get; set; }
		
	}
}
