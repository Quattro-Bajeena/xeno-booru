﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Models
{


	public class Level : Post
	{
		public int LevelId { get; set; }
		public override string FileName => $"Level{LevelId}.glb";

	}
}
