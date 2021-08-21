using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.Security.Api.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Security.Api.Core.Persistence
{
    public class SecurityContext: IdentityDbContext<User>
    {
        public SecurityContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
