using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface ITagRepository
	{
		public IEnumerable<TagEntity> GetAll();
		public IEnumerable<TagEntity> GetFromPost(int postId);
		public void Add(TagEntity tag);
		public void Remove(int id);
		public void Update(TagEntity tag);
		public int GetTagPostCount(int tagId);
		public IEnumerable<TagEntity> GetFromStr(IEnumerable<string> tagsStr);
	}
}
