using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using XenoBooru.Data;
using XenoBooru.Data.Entities;

namespace XenoBooru.DataManipulation
{
	class Settings
	{
		public string DevConnectionString { get; set; }
		public string ProdConnectionString { get; set; }
        public bool Prod { get; set; }
    }

	static class DatabaseOperations
	{
		static void AddMapPosts(AppDbContext db)
        {

			var levelPool = new PoolEntity
			{
				Name = "Levels",
				Description = "Levels from in-game files",
				Creator = "Paraon",
				Created = DateTime.Now,
				Entires = new List<PoolEntryEntity>()
			};

			var levelTag = db.Tags.Where(tag => tag.Name == "level").FirstOrDefault();
			for (int i = 1; i <= 729; i++)
            {
				var post = new PostEntity
				{
					Type = "Model",
					FileName = "level" + i.ToString() + ".glb",
					Name = "Level " + i.ToString(),
					Description = "Level " + i.ToString(),
					Likes = 0,
					Source = "In-game files",
					Pending = false,
					ThumbnailFileName = "LevelThumbnail_" + i.ToString() + ".webp",
					FileNameDownload = "LevelDownload_" + i.ToString() + ".zip",
					Tags = new List<TagEntity>()
				};

				var entryEntity = new PoolEntryEntity
				{
					Post = post,
					Pool = levelPool,
					Position = i
				};
				levelPool.Entires.Add(entryEntity);

				post.Tags.Add(levelTag);

				db.Posts.Add(post);

				Console.WriteLine("Added level " + i);
            }
			db.Pools.Add(levelPool);
			db.SaveChanges();
			Console.WriteLine("Done");

        }
		
		static void AddDownloadLinksToPosts(AppDbContext db)
		{
			var levels = db.Posts.Where(post => post.Type == "Model").ToList();
			foreach (var level in levels)
			{
				var level_id = level.Name.Split(' ')[1];
				level.FileNameDownload = $"LevelDownload_{level_id}.zip";
				Console.WriteLine(level.FileNameDownload);
			}
			db.SaveChanges();
		}

		static void CreateLevelPool(AppDbContext db)
		{
			var levelPool = new PoolEntity
			{
				Name = "Levels",
				Description = "Levels from in-game files",
				Creator = "Paraon",
				Created = DateTime.Now,
				Entires = new List<PoolEntryEntity>()
			};

			int i = 1;
			var levels = db.Posts.Where(post => post.Type == "Model").ToList().OrderBy(post => Int32.Parse(post.Name.Split(' ')[1]));
			foreach (var level in levels)
			{
				var entryEntity = new PoolEntryEntity
				{
					Post = level,
					Pool = levelPool,
					Position = i++
				};
				levelPool.Entires.Add(entryEntity);
				Console.WriteLine(level.Name);
			}

			db.Pools.Add(levelPool);
			db.SaveChanges();
			Console.WriteLine("done");
		}

		static void AddLevelTag(AppDbContext db)
		{
			var levels = db.Posts.Where(post => post.Type == "Model").ToList();
			var levelTag = db.Tags.Where(tag => tag.Name == "level").FirstOrDefault();
			foreach (var level in levels)
			{
				level.Tags.Add(levelTag);

				Console.WriteLine(level.Name);
			}
			db.SaveChanges();
		}

		static void Main(string[] args)
		{
			StreamReader r = new StreamReader("appsettings.json");
			string jsonString = r.ReadToEnd();
			Settings settings = JsonSerializer.Deserialize<Settings>(jsonString);

			string connectionString = settings.Prod ? settings.ProdConnectionString : settings.DevConnectionString;

			var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlServer(connectionString)
				.Options;

			using (AppDbContext db = new AppDbContext(contextOptions))
			{
				AddMapPosts(db);
				//AddDownloadLinksToPosts(db);
				//CreateLevelPool(db);
				//AddLevelTag(db);
			}

			Console.Read();
		}
	}
}
