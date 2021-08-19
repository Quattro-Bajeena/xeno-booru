using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Core.Utilities
{
	public static class TagUtilities
	{
		public static string ToString(IEnumerable<Tag> tags)
		{
			var sb = new StringBuilder();

			foreach (Tag tag in tags)
			{
				sb.Append(tag.Name);
				sb.Append(' ');
			}

			return sb.ToString();
		}
	}
}
