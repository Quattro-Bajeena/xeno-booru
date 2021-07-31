using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Data.Entities
{
	public class UserLikeEntity
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string IpAdress { get; set; }
		public PostEntity Post { get; set; }
	}
}
