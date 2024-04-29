using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }

}
