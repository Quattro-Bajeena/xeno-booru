using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Data.Entities
{
	public class PoolEntryEntity
	{
		public int Id { get; set; }
		public int Position { get; set; }
		public int PostId { get; set; }
		public PostEntity Post { get; set; }
		public int PoolId { get; set; }
		public PoolEntity Pool { get; set; }
	}
}
