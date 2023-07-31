using Microsoft.AspNetCore.Mvc;

namespace mvcpruebaTecnica.Controllers
{
    public class CompraUsuarioController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.usuario = TempData["usuario"].ToString();
            return View();
        }
    }
}
