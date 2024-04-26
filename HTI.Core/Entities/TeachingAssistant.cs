using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class TeachingAssistant
    {
        public int TeachingAssistantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string Cv { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
    }

}
