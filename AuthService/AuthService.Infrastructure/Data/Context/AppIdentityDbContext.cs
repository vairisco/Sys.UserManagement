using AuthServer.Infrastructure.Data.Identity;
using AuthService.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Infrastructure.Data.Context
{
    public class AppIdentityDbContext : IdentityDbContext<User, Role, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // seed data user
            //modelBuilder.Entity<Role>()
            //    .HasData(new Role(Constants.Roles.Create) { Name = Constants.Roles.Create, NormalizedName = Constants.Roles.Create.ToUpper() },
            //    new Role(Constants.Roles.Create) { Name = Constants.Roles.Review, NormalizedName = Constants.Roles.Review.ToUpper() },
            //    new Role(Constants.Roles.Create) { Name = Constants.Roles.Confirm, NormalizedName = Constants.Roles.Confirm.ToUpper() });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Default");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
