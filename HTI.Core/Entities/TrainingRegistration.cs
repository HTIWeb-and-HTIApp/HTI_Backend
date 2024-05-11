using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.Entities
{
    public class TrainingRegistration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int   Id { get; set; }
        public string? CompanyName { get; set; }
        public string? track { get; set; }
        public string? Location { get; set; }
    }
}
