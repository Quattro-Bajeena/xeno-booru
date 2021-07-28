using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{
	public class PoolEntry
	{
		public int Id { get; set; }
		public int Position { get; set; }
		public int PostId { get; set; }
		public Post Post { get; set; }
		public int PoolId { get; set; }
		public Pool Pool { get; set; }

		// maybe they should go into a PoolEntryShowViewModel or sth
		public int? PreviousPostId { get; set; }
		public int? NextPostId { get; set; }
	}
}
