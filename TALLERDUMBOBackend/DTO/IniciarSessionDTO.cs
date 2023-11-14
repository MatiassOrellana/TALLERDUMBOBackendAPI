namespace TALLERDUMBOBackend.DTO
{
    public class IniciarSessionDTO
    {

        /**Estos atributos son del administrador solamente**/
        /**el usuario del administrador**/
        public string? UsuarioLogin { get; set; }

        /**la contraseña del administrador**/
        public string? contraseña { get; set; }

    }
}
