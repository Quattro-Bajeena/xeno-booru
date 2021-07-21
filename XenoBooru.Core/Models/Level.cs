using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{


	public class Level
	{
		public int Id { get; set; }
		public string FileName => $"Level{Id}.glb";
		public string Name { get; set; }
		public string Description { get; set; }
		public string Source { get; set; }
		public int Likes { get; set; }
	}
}
