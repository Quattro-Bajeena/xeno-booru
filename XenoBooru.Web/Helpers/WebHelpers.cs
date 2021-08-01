using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XenoBooru.Web.Helpers
{
	public static class WebHelpers
	{
        public static IEnumerable<object> Pages(int current, int pageCount)
        {
            List<object> pages = new List<object>();
            var delta = 7;

            if (pageCount > 7)
            {
                delta = current > 4 && current < pageCount - 3 ? 2 : 4;
            }

            var startIndex = (int)Math.Round(current - delta / (double)2);
            var endIndex = (int)Math.Round(current + delta / (double)2);

            if (startIndex - 1 == 1 || endIndex + 1 == pageCount)
            {
                startIndex += 1;
                endIndex += 1;
            }

            var to = Math.Min(pageCount, delta + 1);
            for (int i = 1; i <= to; i++)
            {
                pages.Add(i);
            }

            if (current > delta)
            {
                pages.Clear();
                var from = Math.Min(startIndex, pageCount - delta);
                to = Math.Min(endIndex, pageCount);
                for (int i = from; i <= to; i++)
                {
                    pages.Add(i);
                }
            }

            if (pages[0].ToString() != "1")
            {
                if (pages.Count() + 1 != pageCount)
                {
                    pages.Insert(0, "...");
                }
                pages.Insert(0, 1);
            }

            if ((int)pages.Last() < pageCount)
            {
                if (pages.Count() + 1 != pageCount)
                {
                    pages.Add("...");
                }
                pages.Add(pageCount);
            }

            return pages;
        }
    }
}
