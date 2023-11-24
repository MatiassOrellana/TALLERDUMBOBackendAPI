using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TALLERDUMBOBackend.Controladores.Base;
using TALLERDUMBOBackend.Data;
using TALLERDUMBOBackend.DTO;
using TALLERDUMBOBackend.DTO.Usuario;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Controladores
{
    public class AdministradorController : ControladorBase
    {
        private readonly DataContext _context;

        public AdministradorController(DataContext context)
        {
            _context = context;
        }

        /*muestran todos los usuarios*/
        [HttpGet("usuarios")]
        public async Task<ActionResult<List<UsuarioDTO>>> TodosLosUsuarios()
        {
            /*excluyo al administrador*/
            var usuarios = await _context.Usuarios.Where(u => u.RolId == 1).ToListAsync();
            /*se muestran los usuarios con todos los atributos*/
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
        /*Es similar, pero a diferencia que muestra el usuario en especifico con su identificador
         utilizado para editar y/o eliminar*/
        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<UsuarioDTO>> MostrarUsuario(int id)
        {
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.RolId == 1);
            if (usuarioEncontrado == null)
            {
                return NotFound("No se encontró el usuario");
            }
            var usuarioDTO = new UsuarioDTO
            {
                Id = usuarioEncontrado.Id,
                Nombre = usuarioEncontrado.Nombre,
                Apellido = usuarioEncontrado.Apellido,
                Correo = usuarioEncontrado.Correo,
                RUTorDNI = usuarioEncontrado.RUTorDNI,
                PuntosObtenidos = usuarioEncontrado.PuntosObtenidos,
                RolId = usuarioEncontrado.RolId

            };

            return usuarioDTO;
        }

        /*lo mismo que los 2 http anteriores pero utilizando la funcion constains por rut*/
        [HttpGet("usuarios/buscarPorRut/{rut}")]
        public async Task<ActionResult<List<UsuarioDTO>>> BuscarUsuarioPorRut(string rut)
        {
            var usuarios = await _context.Usuarios.Where(u => u.RolId == 1 && u.RUTorDNI.Contains(rut)).ToListAsync();
            if (usuarios.Count() == 0 || usuarios == null)
            {
                return NotFound("No se encontraron usuarios con esos números: " + rut);
            }
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

        /*lo mismo que los 3 http anteriores pero utilizando la funcion constains por correo*/
        [HttpGet("usuarios/buscarPorCorreo/{correo}")]
        public async Task<ActionResult<List<UsuarioDTO>>> BuscarUsuarioPorCorreo(string correo)
        {
            var usuarios = await _context.Usuarios.Where(u => u.RolId == 1 && u.Correo.Contains(correo)).ToListAsync();
            if (usuarios.Count() == 0 || usuarios == null)
            {
                return NotFound("No se encontraron usuarios con esos caracteres: " + correo);
            }
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

        /*Un metodo HTTPPost para agregar y crear a un cliente*/
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] RegistrarClienteDTO registrarCliente)
        {

            try
            {
                /*verificacion de correo para que no se repita con otro existente*/
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == registrarCliente.Correo);
                if (usuarioExistente is not null)
                {
                    return BadRequest("Ya esiste un usuario con ese correo");
                }
                var usuarioCreado = new Usuario
                {
                    Nombre = registrarCliente.Nombre,
                    Apellido = registrarCliente.Apellido,
                    Correo = registrarCliente.Correo,
                    RUTorDNI = registrarCliente.RUTorDNI,
                    PuntosObtenidos = 0,//los puntos son generados automaticamente en 0
                    RolId = 1//el rol siempre es asignado como cliente

                }; 
                
                /*añade al usuario*/
                await _context.Usuarios.AddAsync(usuarioCreado);
                /*guarda los cambios*/
                await _context.SaveChangesAsync();

                return Ok("Usuario agregado");
            }
            catch (Exception ex)
            {

                return BadRequest($"Error en la solicitud: {ex.Message}");
            }

        }
        /*Un metodo HTTPPut para editar a un cliente*/
        [HttpPut("usuarios/{id}/editar")]
        public async Task<ActionResult> Editar(int id, [FromBody] EditarClienteDTO usuarioEditar)
        {
            try
            {
                /*lo busca*/
                var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                /*no lo encontro*/
                if (usuarioEncontrado is null)
                {
                    return NotFound("No se encontró el usuario");
                }

                /*verificacion de correo para que no se repita con otro existente*/
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == usuarioEditar.Correo);
                if (usuarioExistente is not null && usuarioExistente != usuarioEncontrado)
                {
                    return BadRequest("Ya esiste un usuario con ese correo");
                }

                /*cambian los datos del usuario*/
                usuarioEncontrado.Nombre = usuarioEditar.Nombre;
                usuarioEncontrado.Apellido = usuarioEditar.Apellido;
                usuarioEncontrado.Correo = usuarioEditar.Correo;
                usuarioEncontrado.PuntosObtenidos = usuarioEditar.PuntosObtenidos;
                /*guarda los cambios*/
                await _context.SaveChangesAsync();
                return Ok("Los datos del usuario han sido actualizados correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar: {ex.Message}");
            }
        }

        // POST: AdministradorController/Delete/5
        [HttpDelete("usuarios/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                /*lo busca*/
                var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
                /*no lo encontro*/
                if (usuarioEncontrado == null)
                {
                    return NotFound("No se encontró el usuario");
                }
                /*elimina el usuario de la lista*/
                _context.Usuarios.Remove(usuarioEncontrado);
                /*guarda los cambios*/
                await _context.SaveChangesAsync();
                return Ok("Usuario eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar: {ex.Message}");
            }
        }
    }
}
