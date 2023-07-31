using Microsoft.AspNetCore.Mvc;
using mvcpruebaTecnica.Models;
using System.Diagnostics;
using System.Net;
using mvcpruebaTecnica.Servicios;

namespace mvcpruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            
        }


        public IActionResult crearUsuario()
        {
            return View("CreacionUsuarios");

        }


        [HttpGet]
        public void Logueoform(string usuario, string password)
        {
            Servicios.Servicios servicio = new Servicios.Servicios();
            string comodin = servicio.tipousuario(usuario, password);
            if (comodin == "No Existe")
            {
                //lleva a pantalla creacion
                Response.Redirect("/Home/crearUsuario");

            }
            else if(comodin == "password Erroneo")
            {
                //existe pero el pass esta mal, mensaje de error, no puede continuar
            }else
            {
                // aqui el tipo de usuario e ingresa a pantalla
                if (comodin == "1") //administrador
                {
                    Response.Redirect("/Movimiento/Index");
                }
                else
                {
                    TempData["usuario"] = usuario;
                    Servicios.Servicios servicioprod = new Servicios.Servicios();

                    List<Productos> listaprod = new List<Productos>();
                    listaprod = servicioprod.listaproductos();

                    Response.Redirect("/CompraUsuario/Index");
                    // tipo de usuario 2 ingresa por 


                }
            }


  
        }




        [HttpPost]
        public ActionResult Resform(string nombres,string direccion,string telefono,string usuario,string identificacion,string contrasena,int idtipo)
        {
            Servicios.Servicios servicio = new Servicios.Servicios();
            bool  serv= servicio.crearUsuario (nombres, direccion, telefono, usuario, identificacion, contrasena, idtipo);

            return View("CompraUsuario");
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}