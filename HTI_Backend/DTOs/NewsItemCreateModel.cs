namespace HTI_Backend.DTOs
{
    public class NewsItemCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile>? Files { get; set; }
        public IFormFile? CoverPhoto { get; set; } // Cover photo

    }
}
