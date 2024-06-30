namespace HTI_Backend.DTOs
{
    public class StudentsLastTermCoursesDTOs
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        public List<StudentDTO> Studss { get; set; }


    }
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}