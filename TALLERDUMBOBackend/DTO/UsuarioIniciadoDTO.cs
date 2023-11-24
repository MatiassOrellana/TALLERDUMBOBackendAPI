namespace TALLERDUMBOBackend.DTO
{
    /*El usuario iniciado para cargar el token y poder reconocerlo mediante del token*/
    public class UsuarioIniciadoDTO
    {
        public string? token { get; set; }

        public string? UsuarioLogin { get; set; }

        public string? contrasena { get; set; }
    }
}
