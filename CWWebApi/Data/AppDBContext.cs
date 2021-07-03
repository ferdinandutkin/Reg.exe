using Core.Models;
using CWWebApi.Models;
using CWWebApi.Security;
using Microsoft.EntityFrameworkCore;

namespace CWWebApi.Data
{

    public class AppDBContext : DbContext
    {
        public DbSet<InputQuestion> Questions { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<TestResult> Results { get; set; }

        public DbSet<ReferenceEntry> ReferenceEntries { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ApiUser> ApiUsers { get; set; }

        public DbSet<Role> ApiUserRoles { get; set; }
       


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            modelBuilder.Entity<InputQuestion>()
               .Ignore(r => r.ThrownExceptions)
               .Ignore(r => r.Changed)
               .Ignore(r => r.Changing)

               .HasMany(q => q.TestCases)
               .WithOne()
               .IsRequired();

            modelBuilder.Entity<TestCase>()

                .Ignore(r => r.ThrownExceptions)
                .Ignore(r => r.Changed)
                .Ignore(r => r.Changing)

                .HasMany(t => t.Positions)
                .WithOne()
                .IsRequired();

            modelBuilder.Entity<TestResult>()
                .Ignore(r => r.Results)


                .Ignore(r => r.ThrownExceptions)
                .Ignore(r => r.Changed)
                .Ignore(r => r.Changing);



            modelBuilder
                .Entity<User>()
                .Property(u => u.Name).IsRequired();
 

        

            modelBuilder
                .Entity<User>()
                .Ignore(r => r.ThrownExceptions)
                .Ignore(r => r.Changed)
                .Ignore(r => r.Changing)

                .HasIndex(u => u.Name)
                .IsUnique();



            modelBuilder
                .Entity<ApiUser>()
                .HasOne(a => a.User)
                .WithOne()
                .HasPrincipalKey<ApiUser>()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder
                .Entity<ApiUser>()
                .Property(a => a.Password)
                .IsRequired();


        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(@"Data Source=tcp:cwwebapidbserver.database.windows.net,1433;Initial Catalog=App;User Id=adminduck@cwwebapidbserver;Password=milk7322008!");
        }
    }

}
