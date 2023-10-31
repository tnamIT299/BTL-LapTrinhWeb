using Client_Home.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Client_Home.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SalesLeadEntity> SalesLeadEntities { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\PHUHUYIT;Initial Catalog=CONVENIENCESTORE;Integrated Security=True; TrustServerCertificate=True");
        }
    }
}
