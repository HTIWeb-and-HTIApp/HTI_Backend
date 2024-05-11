using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTI.Core.Entities;

namespace HTI.Core.RepositoriesContract
{
    public interface IGraduationRepository
    {
          Task AddGraduationAsync(Team registration);

    }
}
