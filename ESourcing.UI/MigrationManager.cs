using ESourcing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ESourcing.UI
{
    public static class MigrationManager
    {
        public static IHost CreateAndSeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var aspnetRunContext = services.GetRequiredService<WebAppContext>();
                    WebAppContextSeed.SeedAsync(aspnetRunContext, loggerFactory).Wait();
                  
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error occured seeding the DB");
                }
                return host;
            }
        }
    }
}
