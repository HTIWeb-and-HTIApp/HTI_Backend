namespace HTI_Backend.DTOs
{
    public class AllStudentReturnDTO
    {
        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEnrollment { get; set; }
        public int Credits { get; set; }
        public float GPA { get; set; }
        public float Expenses { get; set; }

        public string Department { get; set; }
    }
}
