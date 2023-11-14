using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Data
{
    /**Clase para procesar datos semilla**/
    public class Seed
    {

        /**Metodo para Acceder al proceso de deseriealizacion los archivos que están en formato JSON**/
        public static void SeedData(DataContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CallEachSeeder(context, options);

        }
        /**Lee archivo por archivo en formato Json**/
        public static void CallEachSeeder(DataContext context, JsonSerializerOptions options)
        {
            SeedFirstOrderTables(context, options);
            SeedSecondOrderTales(context, options);

        }

        /**Estos métodos se llamaran primero**/
        private static void SeedFirstOrderTables(DataContext context, JsonSerializerOptions options)
        {
            SeedRoles(context, options);
            context.SaveChanges();
        }

        /**Estos métodos se llamaran Despues**/
        private static void SeedSecondOrderTales(DataContext context, JsonSerializerOptions options)
        {
            SeedUsers(context, options);
            context.SaveChanges();
        }

        /**Método para cargar los roles**/
        private static void SeedRoles(DataContext context, JsonSerializerOptions options)
        {
            //si encuentra algun rol
            var result = context.Roles?.Any();
            if (result is true or null) return;//no lo entontro
            var rolesData = File.ReadAllText("Data/Seeders/Roles.json");//lee el archivo
            var rolesLista = JsonSerializer.Deserialize<List<Rol>>(rolesData, options);//lo deserializa
            if (rolesLista == null) return;//en caso que no hayan roles, pasa nada
            // Eso si siempre habrá una lista de roles
            // El mansaje de advertencia
            if (context.Roles == null) throw new Exception("No hay Roles");
            context.Roles.AddRange(rolesLista);
            context.SaveChanges();
        }


        private static void SeedUsers(DataContext context, JsonSerializerOptions options)
        {
            //si encuentra algun tabla de usuario
            var result = context.Usuarios?.Any();
            if (result is true or null) return;//no lo entontró
            var usuariosData = File.ReadAllText("Data/Seeders/UsuariosIniciales.json");//lee el archivo
            var usuariosLista = JsonSerializer.Deserialize<List<Usuario>>(usuariosData, options);//lo deserializa
            if (usuariosLista == null) return;//en caso que no hayan usuarios, pasa nada
            // Siempre va aver un administrador

            /**Va recorriendo la lista**/
            usuariosLista.ForEach(usuario =>
            {
                if(usuario.contraseña is not null)
                {
                    /**Con el paquete BCrypt.Net encripta la contraseña**/
                    var HashContraseña = BCrypt.Net.BCrypt.HashPassword(usuario.contraseña);
                    usuario.contraseña = HashContraseña;
                }
            });
            if (context.Usuarios == null) throw new Exception("No hay Usuarios");

            context.Usuarios.AddRange(usuariosLista);
            context.SaveChanges();
        }

    }
}
