using Client_Home.Models;
using Microsoft.EntityFrameworkCore;

namespace Client_Home.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SalesLeadEntity> SalesLeadEntities { get; set;}
    }
}
