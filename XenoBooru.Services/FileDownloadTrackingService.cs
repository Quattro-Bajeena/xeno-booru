using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class FileDownloadTrackingService
	{
		private readonly IFileDownloadTrackingRepository _fileDownloads;
		public FileDownloadTrackingService(IFileDownloadTrackingRepository fileDownloads)
		{
			_fileDownloads = fileDownloads;
		}


		public bool Register(string name)
		{
			return _fileDownloads.Register(name);
		}

		public int GetCount(string name)
		{
			return _fileDownloads.GetCount(name);
		}
	}
}
