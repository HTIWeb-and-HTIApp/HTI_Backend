using HTI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.RepositoriesContract
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<Registration>> GetRegistrationsByStudentId(int studentId);
        Task<IEnumerable<Group>> GetGroupsByIds(IEnumerable<int> groupIds);
    }
}
