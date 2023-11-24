using Microsoft.EntityFrameworkCore;
using TALLERDUMBOBackend.Data;

namespace TALLERDUMBOBackend.Extensions
{
    /*Esto es un codigo de servicio que lo que hace es cargar los datos semilla y añadirlos a la base de datos utilizando
     * DataContext*/
    public class AppSeedService
    {
        public static void SeedDatabase(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                
                context.Database.Migrate();
                Seed.SeedData(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, " A problem ocurred during seeding");
            }
        }

    }
}
