using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.Models
{
	public class DbComment
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string Author { get; set; }
		[StringLength(10000)]
		public string Content { get; set; }
	}
}
