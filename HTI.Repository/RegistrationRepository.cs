using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly StoreContext _context;

        public RegistrationRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Group>> GetGroupsByIds(IEnumerable<int> groupIds)
        {
            return await _context.Groups
                .Where(g => groupIds.Contains(g.GroupId))
                .ToListAsync();

        }

        public async Task<IEnumerable<Registration>> GetRegistrationsByStudentId(int studentId)
        {
            return await _context.Registrations
                .Where(r => r.StudentId == studentId && r.IsOpen == true)
                .ToListAsync();
        }
    }
}
