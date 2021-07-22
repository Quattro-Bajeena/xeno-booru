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
			services.AddDbContext<Data.ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);


			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
									builder =>
									{
										//builder.WithOrigins("https://xmvstorage.blob.core.windows.net");
										builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
									});
									
			});

			// services.AddResponseCaching();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();

			
			services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
			services.AddOptions();
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
			app.UseCors();

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
