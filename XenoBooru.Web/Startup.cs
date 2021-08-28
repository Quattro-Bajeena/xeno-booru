using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Data.Repositories;
using XenoBooru.Data.Repositories.Interfaces;
using XenoBooru.Services;
using XenoBooru.Web.Services;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Core.Extensions;

namespace XenoBooru.Web
{
	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;	 
		}

		public IConfiguration Configuration { get; }


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddDbContext<Data.AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("XenoBooru.Data"))
			);
			//options.UseLazyLoadingProxies(). to enable lazy laoding + make prop virtual

			services.Configure<AppOptions>(Configuration.GetSection("AppConfig"));
			services.AddOptions();

			services.AddScoped(x =>
				new BlobContainerClient(Configuration.GetConnectionString("AzureStorage"), Configuration.GetSection("AppConfig")["StorageContainer"])
			);

			services.AddScoped<IPostRepository, SQLPostRepository>();
			services.AddScoped<ICommentRepository, SQLCommentRepository>();
			services.AddScoped<ITagRepository, SQLTagRepository>();
			services.AddScoped<IPoolRepository, SQLPoolRepository>();

			services.AddScoped<PostService>();
			services.AddScoped<CommentService>();
			services.AddScoped<TagService>();
			services.AddScoped<PoolService>();

			services.AddAutoMapper(typeof(PostService));

			services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(options => {
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});

			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(20); //default anyway
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<AuthenticationService>();

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});
			services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
			services.AddAzureClients(builder =>
			{
				builder.AddBlobServiceClient(Configuration["ConnectionStrings:AzureStorage:blob"], preferMsi: true);
				builder.AddQueueServiceClient(Configuration["ConnectionStrings:AzureStorage:queue"], preferMsi: true);
			});
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();

			

			app.UseRouting();

			// app.UseResponseCaching(); after cors

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
	internal static class StartupExtensions
	{
		public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
		{
			if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
			{
				return builder.AddBlobServiceClient(serviceUri);
			}
			else
			{
				return builder.AddBlobServiceClient(serviceUriOrConnectionString);
			}
		}
		public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
		{
			if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
			{
				return builder.AddQueueServiceClient(serviceUri);
			}
			else
			{
				return builder.AddQueueServiceClient(serviceUriOrConnectionString);
			}
		}
	}
}
