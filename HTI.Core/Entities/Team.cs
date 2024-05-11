using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class Team
    {
        
        public int Id { get; set; }
        public bool HasTeam { get; set; }
        public int? NumberOfStudents { get; set; } // Nullable for individual projects  //6

        public ICollection<TeamMember> TeamMembers { get; set; } // Collection of team members
    }
}
