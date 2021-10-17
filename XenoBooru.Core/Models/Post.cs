using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{

	public enum PostType
	{
		Image, Audio, Model
	}

	public class Post
	{

		public Post()
		{
			Created = DateTime.Now;
			Pending = true;
			Likes = 0;
		}

		public int Id { get; set; }
		public PostType Type { get; set; }
		public string FileName { get; set; }
		public string FileNameDownload { get; set; }
		public string ThumbnailFileName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public string Credits { get; set; }
		public int Likes { get; set; }
		public bool Pending { get; set; }
		public DateTime Created { get; set; }
		public int? ParentId { get; set; }
		public Post Parent { get; set; }
		public ICollection<Post> Children { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<PoolEntry> PoolsEntries { get; set; }



		public string DownloadUrl(string containerUrl)
		{
			return containerUrl + "/" + (FileNameDownload ?? FileName);
		}

		public string ThumbnailUrl(string containerUrl, string audioThumbnailFileName)
		{
			string fileUrl = containerUrl + "/";
			if (ThumbnailFileName == null)
			{
				switch (Type)
				{

					case PostType.Image:
						fileUrl += FileName;
						break;
					case PostType.Audio:
						fileUrl += audioThumbnailFileName;
						break;
					default:
					case PostType.Model:
						fileUrl = "";
						break;
				}
			}
			else
			{
				fileUrl += ThumbnailFileName;
			}
			return fileUrl;
		}
	}
}
