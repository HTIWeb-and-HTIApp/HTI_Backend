using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class TeamMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } //42020
        public int TeamId { get; set; } // Foreign key referencing Team
        public string Name { get; set; }
        public string Role { get; set; } // Technology or specialization
        public int Semester { get; set; } // Discussion semester
    }
}
