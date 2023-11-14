using System.ComponentModel.DataAnnotations;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.DTO.Usuario
{
    public class UsuarioDTO
    {
        /**Id del usuario**/
        public int Id { get; set; }
        /**Nombre del usuario mostrado**/
        public string Nombre { get; set; }

        /**Apellido del usuario mostrado**/
        public string Apellido { get; set; }

        /**Correo del usuario mostrado**/
        public string Correo { get; set; }

        /**El rut del usuario mostrado**/
        public string RUTorDNI { get; set; }

        /**Los puntos del cliente mostrados**/
        public int PuntosObtenidos { get; set; }

        /**El rol del usuario implicito**/
        public int? RolId { get; set; }

    }
}
