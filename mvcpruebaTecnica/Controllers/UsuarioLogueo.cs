using Microsoft.AspNetCore.Mvc;
using mvcpruebaTecnica.Models;
using System.Diagnostics;
using System.Net;
using mvcpruebaTecnica.Servicios;

namespace mvcpruebaTecnica.Controllers
{
    public class UsuarioLogueo :Controller
    {

        public IActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public ActionResult Logueoform(string usuario, string password)
        {
            Servicios.Servicios servicio = new Servicios.Servicios();
            string  tipoUsuario = servicio.tipousuario(usuario,password);


            return View();
        }

    }
}
