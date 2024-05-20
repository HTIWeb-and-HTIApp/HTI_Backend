using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhotoUrl { get; set; } // URL for the cover photo

        public List<NewsItemFile>? Files { get; set; } = new();
    }
}
