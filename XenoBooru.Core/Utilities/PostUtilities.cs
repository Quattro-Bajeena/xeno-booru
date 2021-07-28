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

		public static string ThumbnailUrl(string storageUrl, string audioThumbnailFileName, Post post)
		{
			string fileName = post.ThumbnailFileName;
			if(fileName == null)
			{
				if(post.Type == PostType.Artwork)
				{
					fileName = post.FileName;
				}
				else if (post.Type == PostType.Music)
				{
					fileName = audioThumbnailFileName;
				}
				
			}

			return storageUrl + "/" + fileName;
		}
	}
}
