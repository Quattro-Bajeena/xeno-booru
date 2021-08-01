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

		public static string ThumbnailUrl(string containerUrl, string audioThumbnailFileName, Post post)
		{
			string fileUrl = containerUrl + "/";
			if(post.ThumbnailFileName == null)
			{
				switch (post.Type)
				{
					
					case PostType.Artwork:
						fileUrl += post.FileName;
						break;
					case PostType.Audio:
						fileUrl += audioThumbnailFileName;
						break;
					case PostType.Default:
					case PostType.Model:
						fileUrl = "";
						break;
				}

			}
			else
			{
				fileUrl += post.ThumbnailFileName;
			}

			return fileUrl;
		}
	}
}
