using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using pruebaTecnica.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using mvcpruebaTecnicaApi.Models;

namespace pruebaTecnicaApi.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly string conexion;

        public MovimientoController(IConfiguration cadcon)
        {
            conexion = cadcon.GetConnectionString("myconexion");
        }

        [HttpGet]
        [Route("movimiento")]
        public IActionResult movimiento()
        {
            List<Movimiento> usas = new List<Movimiento>();
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("pa_movimientos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usas.Add(new Movimiento()
                            {
                                usuario = reader.GetString("usuario"),
                                producto = reader.GetString("producto"),
                                cantidadcompra = reader.GetString("cantidadcompra").Trim(),
                                disponible = reader.GetString("disponible").Trim()
                                

                            });
                        }
                    }
                }
                return StatusCode(statusCode: 200, usas);


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { response = ex.Message });

            }

        }



    }
}
