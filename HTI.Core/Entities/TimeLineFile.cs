using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class TimeLineFile
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string BlobName { get; set; }
        public string ContentType { get; set; }
        public string SasUrl { get; set; } // Full SAS URL
        public TimeLine TimeLine { get; set; }
    }
}
