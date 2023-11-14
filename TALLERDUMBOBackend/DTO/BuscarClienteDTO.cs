namespace TALLERDUMBOBackend.DTO
{
    public class BuscarClienteDTO
    {
        /**El buscador por rut donde se puede escribir cualquier caracter**/
        public string RUTorDNI { get; set; }

        /**El buscador por correo donde se puede escribir cualquier caracter independiente del arroba**/
        public string Correo { get; set; }

    }
}
