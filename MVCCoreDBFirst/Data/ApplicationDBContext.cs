using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVCCoreDBFirst.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<SiteDataModel> Site { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Server=.\\SQLExpress; Database=CodeFirstDB;Trusted_Connection=True;MultipleActiveResultSets=True; ");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API

            modelBuilder.Entity<SiteDataModel>().HasIndex(s => s.ID).IsUnique(true);
            modelBuilder.Entity<MsysUsersDataModel>().HasIndex(u => u.ID).IsUnique(true);


        }
    }
}
