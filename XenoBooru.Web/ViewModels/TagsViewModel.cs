using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
    public class TagsViewModel
    {
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<object> Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int TagsOnPage { get; set; }
        public TagType? Type { get; set; }
        public TagOrder Order { get; set; }
    }
}
