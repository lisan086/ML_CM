using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ML.PCM.Data;

public class PCMDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public PCMDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the PCMDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PCMDbContext>()
            .Database
            .MigrateAsync();

    }
}
