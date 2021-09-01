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
using XenoBooru.Core.Configuration;
using Microsoft.Net.Http.Headers;

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
			services.AddAzureClients(builder =>
			{
				builder.AddSecretClient(new Uri(Configuration["KeyVaultUri"]));
				builder.AddBlobServiceClient(Configuration.GetConnectionString("AzureStorage"), preferMsi: true);
			});
			services.Configure<AppOptions>(Configuration.GetSection("AppOptions"));
			services.AddOptions();

			services.AddScoped<IPostRepository, SQLPostRepository>();
			services.AddScoped<ICommentRepository, SQLCommentRepository>();
			services.AddScoped<ITagRepository, SQLTagRepository>();
			services.AddScoped<IPoolRepository, SQLPoolRepository>();

			services.AddScoped<PostService>();
			services.AddScoped<CommentService>();
			services.AddScoped<TagService>();
			services.AddScoped<PoolService>();
			services.AddScoped<AzurePostClient>();


			services.AddAutoMapper(typeof(PostService));

			var connection = Configuration.GetConnectionString("DefaultConnection");
			var connectionStrings = Configuration.GetSection("ConnectionStrings");
			services.AddDbContext<Data.AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("XenoBooru.Data"))
			);
			//options.UseLazyLoadingProxies(). to enable lazy laoding + make prop virtual

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<AuthenticationService>();

			services.AddResponseCaching();
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(20); //default anyway
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

			Console.WriteLine(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
			services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);


			services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(options => {
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (!env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Error/Exception");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseStatusCodePagesWithReExecute("/Error/Handle", "?code={0}");
			app.UseHttpsRedirection();

			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = context => context.Context.Response.GetTypedHeaders()
					.CacheControl = new CacheControlHeaderValue
					{
						Public = true,
						MaxAge = TimeSpan.FromDays(365) // 1 year
					}
			});
			app.UseSession();

			app.UseRouting();
			app.UseAuthorization();

			app.UseResponseCaching();

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
	}
}
