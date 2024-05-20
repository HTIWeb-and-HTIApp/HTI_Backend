namespace HTI_Backend.DTOs
{
    public class DoctorCoursesReturnDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        public IEnumerable<course> courses { get; set; }
    }
    public class course
    {
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public int GroupNumber { get; set; }

    }
    //public class GroupDto
    //{
    //    public int GroupId { get; set; }
    //    public int DoctorId { get; set; }
    //    public course Course { get; set; }
    //}
}
