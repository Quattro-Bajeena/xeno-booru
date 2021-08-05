using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Data.Entities
{
	public class CommentEntity
	{

		public int Id { get; set; }
		[StringLength(50)]
		public string Author { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }
		public string IpAddress { get; set; }
		public int PostId { get; set; }
		public PostEntity Post { get; set; }
	}
}
