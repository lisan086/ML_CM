using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ML.PCM.Data;

public class PCMDbContextFactory : IDesignTimeDbContextFactory<PCMDbContext>
{
    public PCMDbContext CreateDbContext(string[] args)
    {
        PCMGlobalFeatureConfigurator.Configure();
        PCMModuleExtensionConfigurator.Configure();
        
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PCMDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new PCMDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}