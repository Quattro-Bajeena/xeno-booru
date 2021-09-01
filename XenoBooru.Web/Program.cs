using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Identity;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace XenoBooru.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((ctx, builder) =>
				{
					if (ctx.HostingEnvironment.IsProduction())
					{
						var builtConfig = builder.Build();
						var keyVaultEndpoint = builtConfig["KeyVaultUri"];
						if (!string.IsNullOrEmpty(keyVaultEndpoint))
						{
							var azureServiceTokenProvider = new AzureServiceTokenProvider();
							var keyVaultClient = new KeyVaultClient(
								new KeyVaultClient.AuthenticationCallback(
									azureServiceTokenProvider.KeyVaultTokenCallback));
							builder.AddAzureKeyVault(
								keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
						}
					}
				})
				.ConfigureLogging((ctx, logging) =>
				{
					logging.AddApplicationInsights(ctx.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
					logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
