using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface IFileDownloadTrackingRepository
	{
		bool Register(string name);
		int GetCount(string name);
	}
}
