using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class StudentCourseHistory
    {
        public int StudentCourseHistoryId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public int DoctorId { get; set; }
        public int TeachingAssistantId { get; set; }
        public float GPA { get; set; }
        public float WorkGrades { get; set; }
        public float FinalGrades { get; set; }
        public float MidtermGrades { get; set; }
        public int StudyYear { get; set; }
        public int Semester { get; set; }

        public bool Status { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
        public Group Group { get; set; }
        public Doctor Doctor { get; set; }
        public TeachingAssistant TeachingAssistant { get; set; }
    }

}
