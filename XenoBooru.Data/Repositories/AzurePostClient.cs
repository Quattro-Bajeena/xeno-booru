using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Configuration;

namespace XenoBooru.Data.Repositories
{
	public class AzurePostClient
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly AppOptions _options;

		public AzurePostClient(BlobServiceClient blobServiceClient, IOptions<AppOptions> options)
		{
			_blobServiceClient = blobServiceClient;
			_options = options.Value;
		}


		public void Upload(string fileName, Stream fileStream)
		{
			var container = _blobServiceClient.GetBlobContainerClient(_options.PostContainer);	
			BlobClient blobClient = container.GetBlobClient(fileName);
			blobClient.Upload(fileStream);
		}

		
	}
}
