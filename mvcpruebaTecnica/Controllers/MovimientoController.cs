using Microsoft.AspNetCore.Mvc;
using mvcpruebaTecnica.Models;

namespace mvcpruebaTecnica.Controllers
{
    public class MovimientoController : Controller
    {
        public IActionResult Index()
        {
            Servicios.Servicios servicio = new Servicios.Servicios();
            List<Movimiento> listamov = new List<Movimiento>();
            listamov = servicio.movimiento();
            return View(listamov);
        }

        


    }
}
