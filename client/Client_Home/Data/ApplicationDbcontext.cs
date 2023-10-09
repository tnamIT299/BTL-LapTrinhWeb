using Client_Home.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Client_Home.Data
{
    public class ApplicationDbcontext : IdentityDbContext
    {
        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SalesLeadEntity> SalesLead { get; set;}
        public DbSet<ApplicationUser> ApplicationUser { get; set;}
    }
}
