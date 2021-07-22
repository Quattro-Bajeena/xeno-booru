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
		PostEntity GetPost(int id);
		IEnumerable<PostEntity> GetAllPosts();

	}
}
