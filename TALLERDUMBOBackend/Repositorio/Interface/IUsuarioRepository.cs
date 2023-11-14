using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Repositorio.Interface
{
    public interface IUsuarioRepository
    {
        /**Desplegar**/
        /**Despliega los usuarios**/
        public Task<List<Usuario>> GetUsuarios();

        /**Metodo para seleccionar al usuario utilizado para editar al cliente**/
        public Task<Usuario> GetUsuarioById(int id);

        /**Utilizado en la busqueda**/
        /**Por Correo**/
        public Task<Usuario> GetUsuarioByCorreo(string correo);
        /**Por Rut**/
        public Task<Usuario> GetUsuarioByRut(string rut);

        /**Agregar**/
        /**Metodo para avisar que se agrega al usuario**/
        public bool AgregarUsuario(Usuario usuario);

       
        /**Editar**/
        /**Metodo para avisar que se edita al usuario y guarda cambios de manera asincrona**/
        public Task<Usuario> ActualizarUsuarioYGuardarCambios(Usuario usuario);

        

        /**Verificaciones**/
        /**Metodo de verificacion si existen los usuarios**/
        /**Por Correo**/
        public Task<bool> ExisteUsuarioPorCorreo(string correo);

        /**Por ID**/
        public Task<bool> ExisteUsuarioPorId(int id);

        /**Por Rut**/
        public Task<bool> ExisteUsuarioPorRut(string rut);

        /**Metodo para avisar que se agrega al usuario y guarda cambios de manera asincrona**/
        public Task<bool> AgregarUsuarioYGuardarCambios(Usuario usuario);

        /**Metodo para avisar que se edita al usuario y guarda cambios de manera asincrona**/
        public Task<bool> UsuarioPudoSerActualizado(Usuario usuario);

        /**Metodo para realizar cambios en la base de datos**/
        public bool GuardarCambios();

        /**Metodo para realizar cambios en la base de datos de manera asincrona**/
        public Task<bool> GuardarCambiosAsync();


    }
}
