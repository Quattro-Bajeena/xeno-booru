using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

			services.AddScoped<IPostRepository, SQLPostRepository>();
			services.AddScoped<PostService>();

			
			services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
			services.AddOptions();

			//var configuration = new MapperConfiguration(cfg =>
			//	cfg.AddProfile<MappingProfile>()
			//);

			services.AddAutoMapper(typeof(PostService));

			services.AddControllersWithViews().AddRazorRuntimeCompilation();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
}
