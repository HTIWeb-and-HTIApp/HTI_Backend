namespace HTI_Backend.DTOs
{
    public class TACoursesReturnDto
    {
        public int TeachingAssistantId { get; set; }
        public string Name { get; set; }
        public IEnumerable<course> courses { get; set; }
    }
   

}
