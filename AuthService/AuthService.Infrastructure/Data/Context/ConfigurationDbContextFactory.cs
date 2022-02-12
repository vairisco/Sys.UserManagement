using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Data.Context
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-N4QO64F\\MSSQL;Database=DemoDb_Wallet;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa;Password=123456;",
                sql => sql.MigrationsAssembly(typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly.GetName().Name));
            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}
