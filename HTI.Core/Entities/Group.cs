using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public int GroupNumber { get; set; }
        public int DoctorId { get; set; }
        public int TeachingAssistantId { get; set; }
        public string LectureRoom { get; set; }
        public string SectionRoom { get; set; }
        public DateTime LectureDate { get; set; }
        public DateTime SectionDate { get; set; }
        public int MaxStudentNumber { get; set; }
        public bool IsOpen { get; set; }
        public Course Course { get; set; }
        public Doctor Doctor { get; set; }
        public TeachingAssistant TeachingAssistant { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
    }

}
