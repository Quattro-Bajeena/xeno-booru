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
		int Add(PostEntity post);
		void Remove(int id);
		void Update(PostEntity updatedPost);
		ICollection<PostEntity> GetByTagsPaged(ICollection<string> tags, bool includePending, bool includeChildren, int page, int onPage);
		int Count(ICollection<string> tags, bool includePending, bool includeChildren);
		IEnumerable<PostEntity> GetFromPool(int poolId);
		void GiveLike(int id, string ip_adress);
		bool UserLiked(int id, string ip_adress);
		void AddChild(PostEntity parent, PostEntity child);
		ICollection<PostEntity> GetChildren(int id);
	}
}
