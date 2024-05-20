using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public  class TimeLine
    {   
        public int TimeLineId { get; set; }
        public int GroupId { get; set; }
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public Group Group { get; set; }
    }
}
