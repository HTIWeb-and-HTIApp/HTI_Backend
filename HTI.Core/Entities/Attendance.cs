using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string CourseCode { get; set; }
        public int WeekNumber { get; set; }
        public bool AttendanceType { get; set; }
        public DateTime AttendanceDate { get; set; }

        public Student Student { get; set; }
        public Group Group { get; set; }
        
    }

}
