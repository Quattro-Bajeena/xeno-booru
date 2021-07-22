using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Data.Entities
{
	public class PostEntity
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public int? TypeId { get; set; }
		public string FileName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Credits { get; set; }
		public int Likes { get; set; }
	}
}
