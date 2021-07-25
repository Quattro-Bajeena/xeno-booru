using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Core.Utilities
{
	public static class PostUtilities
	{
		public static IEnumerable<string> PostTypesToString()
		{
			return Enum.GetValues(typeof(PostType)).Cast<PostType>().Where(type => type != PostType.Default).Select(type => type.ToString());
		}
	}
}
