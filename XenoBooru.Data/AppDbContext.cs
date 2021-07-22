using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Data
{
	public class AppDbContext : DbContext
	{
		private readonly string _connectionString;

		public AppDbContext(DbContextOptions<AppDbContext> options) :
			base(options)
		{

		}

		public DbSet<Entities.PostEntity> Posts { get; set; }
		public DbSet<Entities.CommentEntity> Comments { get; set; }
		public DbSet<Entities.TagEntity> Tags { get; set; }
	}
}
