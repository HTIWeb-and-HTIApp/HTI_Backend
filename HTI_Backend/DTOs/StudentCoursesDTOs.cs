using HTI.Core.Entities;

namespace HTI_Backend.DTOs
{
    public class StudentCoursesRetuenDTO
    {
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsOpen { get; set; }

    }


    public class StudentCoursesRetuenDTOs
    {
        public int StudentId { get; set; }
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
        public int DepartmentId { get; set; }

        public DateTime RegistrationDate { get; set; }
        public bool IsOpen { get; set; }

        public int DoctorId { get; set; }


        public string  DoctorName { get; set; }


    }
}
