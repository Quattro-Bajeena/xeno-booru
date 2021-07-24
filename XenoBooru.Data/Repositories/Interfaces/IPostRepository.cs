using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface IPostRepository
	{
		PostEntity Get(int id);
		IEnumerable<PostEntity> GetAll();
		public void Add(PostEntity post);
		public void Remove(int id);
		public void Update(PostEntity post);
		public IEnumerable<PostEntity> GetByTags(ICollection<string> tags);
	}
}
