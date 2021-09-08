using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Data.Repositories
{
	public class SQLFileDownloadTrackingRepository : IFileDownloadTrackingRepository
	{
		private readonly AppDbContext _context;
		public SQLFileDownloadTrackingRepository(AppDbContext context)
		{
			_context = context;
		}

		
		public bool Register(string name)
		{
			var downloadRecord = _context.FileDownloads.FirstOrDefault(x => x.Name == name);
			if (downloadRecord != null)
			{
				downloadRecord.Count += 1;
				_context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public int GetCount(string name)
		{
			var downloadRecord = _context.FileDownloads.FirstOrDefault(x => x.Name == name);
			if(downloadRecord != null)
			{
				return downloadRecord.Count;
			}
			else
			{
				return 0;
			}
		}


	}
}
