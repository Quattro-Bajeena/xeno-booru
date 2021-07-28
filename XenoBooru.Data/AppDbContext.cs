using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data
{
	public class AppDbContext : DbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) :
			base(options)
		{

		}



		public DbSet<PostEntity> Posts { get; set; }
		public DbSet<CommentEntity> Comments { get; set; }
		public DbSet<TagEntity> Tags { get; set; }
		public DbSet<PoolEntity> Pools { get; set; }
		public DbSet<PoolEntryEntity> PoolEntries { get; set; }

	}
}
