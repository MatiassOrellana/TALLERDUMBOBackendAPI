using Microsoft.EntityFrameworkCore;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Data
{
    public class DataContext : DbContext
    {
        /**DE AQUI se crean las migraciones**/
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /**Se crean las bases de datos**/
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
       
        /**Este metodo no es necesario pero sirve para construir relaciones de N a N y transformar fechas y/u horas**/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
