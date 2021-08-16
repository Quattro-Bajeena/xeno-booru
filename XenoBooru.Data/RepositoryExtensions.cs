using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data
{
	public static class RepositoryExtensions
	{
		public static IQueryable<T> ByPending<T>(this IQueryable<T> posts, bool includePending) where T : PostEntity
		{
			if (includePending)
			{
				return posts;
			}
			else
			{
				return posts.Where(post => post.Pending == false);
			}
		}

		public static IQueryable<T> IncludeChildren<T>(this IQueryable<T> posts, bool includeChildren) where T : PostEntity
		{
			if (includeChildren)
			{
				return posts;
			}
			else
			{
				return posts.Where(post => post.ParentId == null);
			}
		}
	}
}
