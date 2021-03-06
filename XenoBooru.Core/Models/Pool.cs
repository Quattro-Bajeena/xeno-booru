using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{
	public class Pool
	{
		public Pool()
		{
			Created = DateTime.Now;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Creator { get; set; }
		public string Description { get; set; }
		public bool Hidden { get; set; }
		public DateTime Created { get; set; }
		public ICollection<PoolEntry> Entires { get; set; }
	}
}
