using System.ComponentModel.DataAnnotations;

namespace TALLERDUMBOBackend.DTO
{
    public class EditarClienteDTO
    {

        /**Nombre del usuario obligatorio con un minimo de 2 caracteres para editar**/
        [Required(ErrorMessage = "Se requiere el Nombre")]
        [MinLength(2, ErrorMessage = "No creo que exista un nombre con un solo caracter")]
        public string Nombre { get; set; }


        /**Apellido del usuario obligatorio con un minimo de 2 caracteres para editar**/
        [Required(ErrorMessage = "Se requiere el Apellido")]
        [MinLength(2, ErrorMessage = "No creo que exista un apellido con un solo caracter")]
        public string Apellido { get; set; }

        /**Correo del usuario obligatorio en formato xxx@x.x, debe ser único, pero eso se hara en el patron repositorio**/
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [Required(ErrorMessage = "Se requiere el correo")]
        public string Correo { get; set; }

        /**Los puntos del cliente no pueden ser negativos se deben validar**/
        [Range(0, int.MaxValue, ErrorMessage = "Los puntos no pueden ser negativos")]
        public int PuntosObtenidos { get; set; }
    }
}
