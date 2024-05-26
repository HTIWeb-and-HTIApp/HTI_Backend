namespace HTI_Backend.DTOs
{

    public class TimeLineCreateDTO
    {
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime Deadline { get; set; }


    }

    public class TimeLineUpdateDTO
    {
        public int TimeLineId { get; set; }
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class TimeLineReturnDTO
    {
        public int TimeLineId { get; set; }
        public int GroupId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public List<TimeLineFileDTO> Files { get; set; }

    }
    public class TimeLineFileDTO
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string ContentType { get; set; }
        public string SasUrl { get; set; }
    }
    public class CourseWithTimeLineDTO
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public List<TimeLineReturnDTO> TimeLines { get; set; }
    }
    public class StudentCoursesWithTimeLinesDTO
    {
        public int StudentId { get; set; }
        public List<CourseWithTimeLineDTO> Courses { get; set; }
    }

}
