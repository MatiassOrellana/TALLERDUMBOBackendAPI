using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TALLERDUMBOBackend.Controladores.Base;
using TALLERDUMBOBackend.Data;
using TALLERDUMBOBackend.DTO;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Controladores
{
    public class AutentificacionController : ControladorBase

    {
        private readonly DataContext _context;/*usa el datacontext*/
        private readonly IConfiguration _configuration;/*ocupa la configuracion para poder generar el token*/
        private UsuarioIniciadoDTO usuarioIniciadoDTO;/*usuario conectado, esto solo funciona si hay 1 solo usuario*/
        public AutentificacionController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            usuarioIniciadoDTO = new UsuarioIniciadoDTO();
        }

        /*HTTPPost porque se está enviando los parametros del usuario y la contraseña y eso se debe verificar*/
        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioIniciadoDTO>> IniciarSession(IniciarSessionDTO iniciarSessionDTO)
        {
            /*busca al usuario con esos parametros*/
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(
                u => u.UsuarioLogin == iniciarSessionDTO.UsuarioLogin);

            /*si no encontro el usuario*/
            if (usuario is null) return BadRequest("Las credenciales son incorrectas, perdedor!!!");

            /*la verificacion del usuario con respecto a la contraseña*/
            var result = BCrypt.Net.BCrypt.Verify(iniciarSessionDTO.contraseña, usuario.contraseña);

            /*salio mal*/
            if(!result) return BadRequest("Las credenciales son incorrectas, perdedor!!!");

            /*llama a crear el token almacenado en al variable token*/
            var token = CreateToken(usuario);

            /*se actualiza el usuario para que sea reconocible*/
            usuarioIniciadoDTO = new UsuarioIniciadoDTO()
            {
                token = token,
                UsuarioLogin = usuario.UsuarioLogin,
                contrasena = usuario.contraseña 
            };

            /*retorna el usuario*/
            return usuarioIniciadoDTO;
        }

        /*metodo privado para generar el token*/
        private string CreateToken(Usuario usuario)
        {
            /*crea una lista de claims*/
            var claims = new List<Claim>
            {
                new ("Usuario", usuario.UsuarioLogin),
                new ("RolId", usuario.RolId.ToString()),
                
            };

            /*apunta en el archivo de appsetting,json*/
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            /*ocupa la credencial de la variable key y de tipo hmacsha256*/
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            /*Ese token es construido con los claims, su expiracion y las credenciales*/
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds

            );

            /*escribe el token*/
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            /*retorna el token*/
            return jwt;
        }

        [HttpPost("Logout")]
        public ActionResult CerrarSesion()
        {
            /*resetea al usuario*/
            usuarioIniciadoDTO = new UsuarioIniciadoDTO();

            return Ok("Sesión cerrada exitosamente");
        }
    }
}
