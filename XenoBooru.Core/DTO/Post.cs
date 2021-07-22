using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.DTO
{

	public enum PostType
	{
		Level, Artwork, Music
	}

	public class Post
	{
		public int Id { get; set; }
		public PostType Type { get; set; }
		public int? TypeId { get; set; }
		public virtual string FileName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public int Likes { get; set; }

		
	}
}
