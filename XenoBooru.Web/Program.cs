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
					var builtConfig = builder.Build();
					var keyVaultEndpoint = builtConfig.GetConnectionString("KeyVault");
					if (!string.IsNullOrEmpty(keyVaultEndpoint))
					{
						var azureServiceTokenProvider = new AzureServiceTokenProvider();
						var keyVaultClient = new KeyVaultClient(
							new KeyVaultClient.AuthenticationCallback(
								azureServiceTokenProvider.KeyVaultTokenCallback));
						builder.AddAzureKeyVault(
							keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
					}

				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});

		public static string GetKeyVaultEndpoint() => "https://xenobooru-keyvault.vault.azure.net/";
	}
}
