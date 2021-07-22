using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Web.Models;
namespace XenoBooru.Web.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
			base(options)
		{

		}

		public DbSet<DbPost> Maps { get; set; }
		public DbSet<DbComment> Comments { get; set; }
		public DbSet<DbTag> Tags { get; set; }
	}
}
