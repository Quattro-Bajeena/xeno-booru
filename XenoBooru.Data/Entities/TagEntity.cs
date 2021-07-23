using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Data.Entities
{
	public class TagEntity
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		//public ICollection<PostEntity> Posts { get; set; }

	}
}
