using Microsoft.AspNetCore.Mvc;
using TALLERDUMBOBackend.Controladores.Base;

namespace TALLERDUMBOBackend.Controladores
{
    public class AutentificacionController : ControladorBase

    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
