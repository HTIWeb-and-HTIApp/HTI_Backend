namespace HTI_Backend.DTOs
{
    public class StudentOpenReqCoursesReturnDTO
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public int StudyYear { get; set; }
        public int Semester { get; set; }
        public bool Lap { get; set; }
        public string Type { get; set; }
        public int? PrerequisiteId { get; set; }
        public bool Status { get; set; }
    }
}
