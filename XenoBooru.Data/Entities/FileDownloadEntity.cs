using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace XenoBooru.Data.Entities
{
	public class FileDownloadEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Count { get; set; }
	}
}
