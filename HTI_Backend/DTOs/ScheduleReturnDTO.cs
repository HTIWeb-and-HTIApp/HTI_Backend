namespace HTI_Backend.DTOs
{
   public class ScheduleReturnDTO
    {
        public string CourseName { get; set; }
        public string CourseType { get; set; }
        public string CourseCode { get; set; }

        public string Room { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
