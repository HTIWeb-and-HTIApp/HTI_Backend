using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class TimeLine
    {
        public int TimeLineId { get; set; }
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime Deadline { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public List<TimeLineFile>? Files { get; set; } = new();
        public Group Group { get; set; }

        public ICollection<Solution> Solutions { get; set; }

    }
}
