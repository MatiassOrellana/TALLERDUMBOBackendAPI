using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TALLERDUMBOBackend.Models
{
    public class Usuario
    {
        /**Id del usuario**/
        [Key]
        public int Id { get; set; }

        /**Nombre del usuario obligatorio entre 2 y 30 caracteres**/
        public string Nombre { get; set; }

        /**Apellido del usuario obligatorio entre 2 y 30 caracteres**/
        public string Apellido { get; set; }

        /**Correo del usuario obligatorio en formato xxx@x.x, debe ser único**/
        public string Correo { get; set; }

        /**El rut del usuario obligatorio y se debe validar y tambien debe ser unico**/
        public string RUTorDNI { get; set; }

        /**Los puntos del cliente no pueden ser negativos se deben validar**/
        public int PuntosObtenidos { get; set; }

        /**Estos atributos son del administrador solamente**/
        /**el usuario del administrador**/
        public string? UsuarioLogin { get; set; }

        /**la contraseña del administrador**/
        public string? contraseña { get; set; }

        /**El rol del usuario**/
        public int RolId { get; set; }

        /**El rol del usuario como objeto**/
        public Rol? Rol { get; set; }
    }
}
