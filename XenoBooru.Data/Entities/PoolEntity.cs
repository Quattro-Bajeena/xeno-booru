using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XenoBooru.Data.Entities
{
	public class PoolEntity
	{
		public int Id { get; set; }
		[StringLength(200)]
		public string Name { get; set; }
		[StringLength(50)]
		public string Creator { get; set; }
		public string Description { get; set; }
		public bool Hidden { get; set; }
		public DateTime Created { get; set; }
		public ICollection<PoolEntryEntity> Entires { get; set; }
	}
}
