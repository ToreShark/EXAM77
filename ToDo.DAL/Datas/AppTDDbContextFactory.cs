using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ToDo.DAL.Datas;

public class AppTDDbContextFactory : IDesignTimeDbContextFactory<AppTDDbContext>
{
    public AppTDDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppTDDbContext>();
        var connectionString = configuration.GetConnectionString("AppTDDbContext");

        optionsBuilder.UseSqlServer(connectionString);

        return new AppTDDbContext(optionsBuilder.Options);
    }
}