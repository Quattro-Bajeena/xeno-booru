using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
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

			db.Pools.Add(levelPool);
			db.SaveChanges();
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
				db.SaveChanges();
				Console.WriteLine("Added level " + i);
            }

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

		static void AddFeaturedPool(AppDbContext db)
		{
			var pool = new PoolEntity
			{
				Name = "Featured maps",
				Creator = "Paraon",
				Created = DateTime.Now,
				Description = "The coolest and most interesting maps"
			};

			db.Pools.Add(pool);
			db.SaveChanges();
		}

		static void AddLevelsToFeaturedPool(AppDbContext db)
		{
			var pool = db.Pools.Where(pool => pool.Id == 2).Include(pool => pool.Entires).FirstOrDefault();
			if (pool.Entires == null)
			{
				pool.Entires = new List<PoolEntryEntity>();
			}
			else
			{
				db.PoolEntries.RemoveRange(pool.Entires);
				pool.Entires.Clear();
			}
			
				

			
			Console.WriteLine(pool.Name);

			var level_ids = new int[]{ 
				17, 26, 35,52,59,71,89,93,94,95,98,196,198,199,237,273,280,290,295,321,333,379,381,383, 390,400,
				405,420,424,434,483,495,507,509,511,515,518,579,619,622,624,625,
				626,638,652,653,655,657,660,661,666,667,669,671,700,702,706,712,717,718
			};
			int count = level_ids.Count();
			Console.WriteLine(level_ids);

			int i = 1;
			foreach (var level_id in level_ids)
			{
				var level = db.Posts.Where(post => post.Name == $"Level {level_id}").FirstOrDefault();
				Console.WriteLine(level.Description);
				var entryEntity = new PoolEntryEntity
				{
					Post = level,
					Pool = pool,
					Position = i
				};
				i++;
				pool.Entires.Add(entryEntity);
			}
			db.SaveChanges();
		}

		static void AddTagsToPost(AppDbContext db)
		{
			//var id = 347;
			var from = 1;
			var to = 100;
			var id_range = Enumerable.Range(from, to + 1 - from);
			var to_add = new string[] {  };
			var to_remove = new string[] { "outdoors" };

			foreach (var map_id in id_range)
			{
				var post = db.Posts.Where(post => post.Id == map_id).Include(post => post.Tags).Single();
				foreach (var name in to_add)
				{
					var tag = db.Tags.Where(tag => tag.Name == name).Single();
					post.Tags.Add(tag);
				}
				foreach (var name in to_remove)
				{
					var tag = db.Tags.Where(tag => tag.Name == name).Single();
					post.Tags.Remove(tag);
				}
			}
			
			db.SaveChanges();
		}

		static void SetPostDates(AppDbContext db)
		{
			var posts = db.Posts.OrderBy(post => post.Id).ToList();
			foreach (var post in posts)
			{
				Console.WriteLine(post.Id);
				post.Created = DateTime.Now;
				Thread.Sleep(10);
				
			}
			db.Posts.UpdateRange(posts);
			db.SaveChanges();
		}

		static void CreateFileDownloads(AppDbContext db)
		{
			var names = new string[] { "AllMaps", "MapViewer", "SceneModels", "HeadsSlides" };
			foreach (var name in names)
			{
				var downloads = db.FileDownloads.FirstOrDefault(download => download.Name == name);
				if (downloads == null)
				{
					downloads = new FileDownloadEntity
					{
						Name = name,
						Count = 0
					};
					db.FileDownloads.Add(downloads);
					Console.WriteLine("File Download tracking added: " + name);
				}
				else
				{
					Console.WriteLine("File Download tracking already exists: " + name);
				}
			}
			db.SaveChanges();
		}

		static void AddSceneModelPosts(AppDbContext db)
		{
			var sceneModelTag = db.Tags.Where(tag => tag.Name == "scene_model").FirstOrDefault();

			for (int i = 0; i <= 71; i++)
			{
				var post = db.Posts.FirstOrDefault(post => post.Name == "Scene Model " + i.ToString());
				if(post != null)
				{
					post.Created = DateTime.Now;
					db.SaveChanges();
					Console.WriteLine("Updated time scene model " + i);
					continue;
				}

				post = new PostEntity
				{
					Type = "Model",
					FileName = "scenemodel" + i.ToString() + ".glb",
					Name = "Scene Model " + i.ToString(),
					Description = "Scene Model " + i.ToString(),
					Likes = 0,
					Source = "In-game files",
					Pending = false,
					Created = DateTime.Now,
					ThumbnailFileName = "SceneModelThumbnail_" + i.ToString() + ".webp",
					FileNameDownload = "scenemodel" + i.ToString() + ".zip",
					Tags = new List<TagEntity>()
				};

				post.Tags.Add(sceneModelTag);
				db.Posts.Add(post);
				db.SaveChanges();
				Console.WriteLine("Added scene model " + i);
			}
			
		}

		static void AddStagePosts(AppDbContext db)
		{
			var stageTag = db.Tags.Where(tag => tag.Name == "stage").FirstOrDefault();

			for (int i = 0; i <= 74; i++)
			{
				var post = new PostEntity
				{
					Type = "Model",
					FileName = "stage" + i.ToString() + ".glb",
					Name = "Battle Stage " + i.ToString(),
					Description = "Battle Stage " + i.ToString(),
					Likes = 0,
					Source = "In-game files",
					Pending = false,
					Created = DateTime.Now,
					ThumbnailFileName = "StageThumbnail_" + i.ToString() + ".webp",
					FileNameDownload = "stage" + i.ToString() + ".zip",
					Tags = new List<TagEntity>()
				};

				post.Tags.Add(stageTag);
				db.Posts.Add(post);
				db.SaveChanges();
				Console.WriteLine("Added stage " + i);
			}
		}

		static void AddSlidesHeads(AppDbContext db)
		{
			var portraitTag = db.Tags.Where(tag => tag.Name == "portrait").Single();
			var slideTag = db.Tags.Where(tag => tag.Name == "slide").Single();

			// slides
			for (int i = 0; i<=87; i++)
			{
				var post = new PostEntity
				{
					Type = "Image",
					FileName = "slide" + i.ToString() + ".png",
					Name = "Slide " + i.ToString(),
					Description = "Slide " + i.ToString(),
					Likes = 0,
					Source = "In-game files",
					Pending = false,
					Created = DateTime.Now,
					Tags = new List<TagEntity>()
				};

				post.Tags.Add(slideTag);
				db.Posts.Add(post);
				db.SaveChanges();
				Console.WriteLine("Added slide " + i);
			}

			// portaits
			for (int i = 0; i <= 90; i++)
			{
				var post = new PostEntity
				{
					Type = "Image",
					FileName = "head" + i.ToString() + ".png",
					Name = "Portrait " + i.ToString(),
					Description = "Portrait " + i.ToString(),
					Likes = 0,
					Source = "In-game files",
					Pending = false,
					Created = DateTime.Now,
					ThumbnailFileName = "HeadThumbnail_" + i.ToString() + ".webp",
					Tags = new List<TagEntity>()
				};

				post.Tags.Add(portraitTag);
				db.Posts.Add(post);
				db.SaveChanges();
				Console.WriteLine("Added portrait " + i);
			}
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
				//AddMapPosts(db);
				//AddDownloadLinksToPosts(db);
				//CreateLevelPool(db);
				//AddLevelTag(db);
				//AddFeaturedPool(db);
				//AddLevelsToFeaturedPool(db);
				//AddTagsToPost(db);
				//SetPostDates(db);
				//CreateFileDownloads(db);
				//AddSceneModelPosts(db);
				//AddStagePosts(db);
				//AddSlidesHeads(db);
			}

			Console.WriteLine("Done");
			//Console.Read();
		}
	}
}
