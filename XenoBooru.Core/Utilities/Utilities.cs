using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenoBooru.Core.Utilities
{
    public class Utilities
    {

        public static IEnumerable<string> EnumToStrings<T>() where T : System.Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T[]>().Select(e => e.ToString());
        }
    }
}
