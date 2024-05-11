namespace HTI_Backend.DTOs
{
    public class UserToReturnDto
    {
        public string Id { get; set; }

        public IEnumerable<string> Role { get; set; }

        public string Token { get; set; }
    }
}
