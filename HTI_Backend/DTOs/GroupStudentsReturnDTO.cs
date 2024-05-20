using HTI.Core.Entities;

namespace HTI_Backend.DTOs
{
    public class GroupStudentsReturnDTO
    {   
 
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        public string DoctorName { get; set; }
        public string TeachingAssistantName { get; set; }
        public IEnumerable<student> Studs { get; set; }


    }

    public class student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }




}
