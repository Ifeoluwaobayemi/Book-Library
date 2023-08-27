using Library.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Library.Web.Data
{
    public class MVCContext : IdentityDbContext<AppUser>
    {
        public MVCContext(DbContextOptions<MVCContext> options) : base(options)
        {

        }
    }
}
