namespace HTI_Backend.DTOs
{
    public class TimeLineDTOs
    {
    }
    public class TimeLineCreateDTO
    {
        public int GroupId { get; set; }
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }      
    }

    public class TimeLineUpdateDTO
    {
        public int GroupId { get; set; }
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }

    public class TimeLineReturnDTO
    {
        public int TimeLineId { get; set; }
        public int GroupId { get; set; }
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}
