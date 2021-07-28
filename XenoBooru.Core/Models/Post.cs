using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{

	public enum PostType
	{
		Default, Artwork, Music, Model
	}

	public class Post
	{

		public Post()
		{
			Pending = true;
		}

		public int Id { get; set; }
		public PostType Type { get; set; }
		public string FileName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public string Credits { get; set; }
		public int Likes { get; set; }
		public bool Pending { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<PoolEntry> PoolsEntries { get; set; }

		
	}
}
