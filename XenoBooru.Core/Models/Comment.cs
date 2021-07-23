using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{
	public class Comment
	{

		public Comment()
		{
			Date = DateTime.Now;
		}


		public int Id { get; set; }
		public string Author { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }
		public Post Post { get; set; }
	}
}
