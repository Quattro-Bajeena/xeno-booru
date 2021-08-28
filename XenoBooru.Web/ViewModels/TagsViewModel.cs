using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XenoBooru.Core.Models;

namespace XenoBooru.Web.ViewModels
{
    public class TagsViewModel : PagedViewModel
    {
        public IEnumerable<Tag> Tags { get; set; }
        public TagType? Type { get; set; }
        public TagOrder Order { get; set; }
    }
}
