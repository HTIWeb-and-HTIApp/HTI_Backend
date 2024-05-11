using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace HTI.Repository
{
    public class trainningResiteration: ItrainningResiteration
    {
        private readonly StoreContext _context;

        public trainningResiteration(StoreContext context)
        {
            _context = context;
        }

        public async Task AddTrainingRegistrationAsync(TrainingRegistration registration)
        {
            _context.TrainingRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }
    }

}
