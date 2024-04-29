using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsOpen { get; set; }

      
        public Student Student { get; set; }
        public Group Group { get; set; }
    }

}
