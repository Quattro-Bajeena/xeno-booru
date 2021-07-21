using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XMV.Models
{
	public class Post
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Category { get; set; }
		public string FileName { get; set; }
		public string Description { get; set; }
	}
}
