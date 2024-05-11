using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HTI.Core
{
    public interface IAuthService
    {
         Task<string>CreateTokenAsync(IdentityUser user , UserManager<IdentityUser> userManager);
    }
}
