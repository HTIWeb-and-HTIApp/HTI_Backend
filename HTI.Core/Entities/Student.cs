using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public Department Department { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
