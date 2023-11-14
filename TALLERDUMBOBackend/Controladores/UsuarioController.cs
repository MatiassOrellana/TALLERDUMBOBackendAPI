using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALLERDUMBOBackend.Controladores.Base;
using TALLERDUMBOBackend.Data;
using TALLERDUMBOBackend.DTO.Usuario;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Controladores
{
    public class UsuarioController : ControladorBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> TodosLosUsuarios()
        {
            var usuarios = await _context.Usuarios.Where(u => u.RolId == 1).ToListAsync();
            var usuariosDTO = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Correo = u.Correo,
                RUTorDNI = u.RUTorDNI,
                PuntosObtenidos = u.PuntosObtenidos,
                RolId = u.RolId
                
            }).ToList();

            return usuariosDTO;

        }

    }
}
