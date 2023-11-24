using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALLERDUMBOBackend.Controladores.Base;
using TALLERDUMBOBackend.Data;
using TALLERDUMBOBackend.DTO.Usuario;
using TALLERDUMBOBackend.Models;

namespace TALLERDUMBOBackend.Controladores
{
    /*aun que no creo que sea necesario el usuario.... porque el que gestiona es el administrador, no el
     usuario en si*/
    public class UsuarioController : ControladorBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

    }
}
