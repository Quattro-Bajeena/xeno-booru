using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface ICommentRepository
	{
		IEnumerable<CommentEntity> GetAll();
		IEnumerable<CommentEntity> GetFromPost(int postId);
		void Add(CommentEntity comment);
		void Remove(int id);

	}
}
