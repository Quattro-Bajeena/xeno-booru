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
		public IEnumerable<CommentEntity> GetAll();
		public IEnumerable<CommentEntity> GetFromPost(int postId);
		public void Add(CommentEntity comment);
		public void Remove(int id);

	}
}
