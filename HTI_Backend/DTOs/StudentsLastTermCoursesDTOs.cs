namespace HTI_Backend.DTOs
{
    public class StudentsLastTermCoursesDTOs
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        public IEnumerable<student> Studss { get; set; }


    }
}