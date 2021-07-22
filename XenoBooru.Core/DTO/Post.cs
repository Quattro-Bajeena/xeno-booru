﻿using System;
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
		public string Type { get; set; }
		public string FileName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public string Credits { get; set; }
		public int Likes { get; set; }
		public  ICollection<Tag> Tags { get; set; }


	}
}
