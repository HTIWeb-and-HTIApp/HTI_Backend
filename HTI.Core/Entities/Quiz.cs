using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
     public class Quiz
    {
        public int QuizId { get; set; }
        public int StudentCourseHistoryId { get; set; }
        public string QuizName { get; set; }
        public DateTime QuizDate { get; set; }
        public float QuizGrade { get; set; }
        public StudentCourseHistory StudentCourseHistory { get; set; }

    }
}
