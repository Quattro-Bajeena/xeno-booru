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
		[Required]
		public string Type { get; set; }
		[Required]
		public string FileName { get; set; }
		public string Name { get; set; }
		public string Source { get; set; }
		public string Description { get; set; }
		public string Credits { get; set; }
		public int Likes { get; set; }
		public bool Pending { get; set; }

		public ICollection<TagEntity> Tags { get; set; }
		public ICollection<CommentEntity> Comments { get; set; }
		public ICollection<PoolEntity> Pools { get; set; }
	}
}
