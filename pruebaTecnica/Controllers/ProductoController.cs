using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using pruebaTecnica.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace pruebaTecnica.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly string conexion;

        public ProductoController(IConfiguration cadcon)
        {
            conexion = cadcon.GetConnectionString("myconexion");
        }

        [HttpGet]
        [Route("listado")]
        public IActionResult listado()
        {
            List<Producto> usas = new List<Producto>();
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("pa_listaproductos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usas.Add(new Producto()
                            {
                                id = reader.GetInt16("id"),
                                nombre = reader.GetString("nombre"),
                                disponible = reader.GetInt16("disponible"),
                                descripcion = reader.GetString("descripcion")
                              
                            });
                        }
                    }
                }
                return StatusCode(statusCode: 200, usas);


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { ex.Message });

            }

        }



    }
}
