using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{
	public enum TagType { Normal, Location, PostType, Character }
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public TagType Type { get; set; }
		public ICollection<Post> Posts { get; set; }
		public int PostCount { get; set; }

		public override string ToString() => Name.Replace('_', ' ');
	}
}
