using Library.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.API.Data.Context
{
    public class LibraryContext : IdentityDbContext<AppUser>
    {
        
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
