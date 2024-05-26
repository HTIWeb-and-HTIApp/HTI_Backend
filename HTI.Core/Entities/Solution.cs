using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Solution
    {
        public int SolutionId { get; set; }
        public int StudentId { get; set; }
        public int TimeLineId { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmissionDate { get; set; }

        public Student Student { get; set; }
        public TimeLine TimeLine { get; set; }
    }
}
