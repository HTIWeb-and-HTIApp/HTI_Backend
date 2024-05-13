namespace HTI_Backend.DTOs
{
    public class GroupCreateDTO
    {
        public int CourseId { get; set; }
        public int GroupNumber { get; set; }
        public int DoctorId { get; set; }
        public int TeachingAssistantId { get; set; }
        public string LectureRoom { get; set; }
        public string SectionRoom { get; set; }
        public DateTime LectureDate { get; set; }
        public DateTime SectionDate { get; set; }
        public int MaxStudentNumber { get; set; }
        public bool IsOpen { get; set; } = true;

    }

    public class GroupReturnDTO
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

    }

    public class GroupUpdateDTO
    {
        public int GroupNumber { get; set; }
        public int DoctorId { get; set; }
        public int TeachingAssistantId { get; set; }
        public string LectureRoom { get; set; }
        public string SectionRoom { get; set; }
        public DateTime LectureDate { get; set; }
        public DateTime SectionDate { get; set; }
        public int MaxStudentNumber { get; set; }
    }
}