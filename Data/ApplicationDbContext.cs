using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AstoundContactsWebApp.Models;

namespace AstoundContactsWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //ToDo seed user login
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AstoundContactsWebApp.Models.Contact> Contact { get; set; }
        public DbSet<AstoundContactsWebApp.Models.Link> Link { get; set; }
     
    }
}