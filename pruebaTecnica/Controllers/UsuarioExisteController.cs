using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using pruebaTecnica.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using pruebaTecnicaAPI.Models;

namespace pruebaTecnica.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioExisteController : ControllerBase
    {
        private readonly string conexion;

        public UsuarioExisteController(IConfiguration cadcon)
        {
            conexion = cadcon.GetConnectionString("myconexion");
        }

        [HttpGet]
        [Route("existeusuario/{usuario}")]
        public IActionResult existeusuario(string usuario,string password)
        {
            List<UsuarioExiste> usas = new List<UsuarioExiste>();
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("pa_existeusuario", conn);
                    cmd.Parameters.AddWithValue("usuario", usuario);
                    cmd.Parameters.AddWithValue("contrasena", password);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usas.Add(new UsuarioExiste()
                            {
                                existe = reader.GetInt32("conteo"),// el usuario existe
                                idtipo = reader.GetInt32("idtipo"),//el tipo de usuario 1=admin, 2 usuario normal
                                sucess = reader.GetInt32("sucess") // si el password es correcto
                                
                            });
                        }
                    }
                }
                return StatusCode(statusCode: 200, new {usas });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { ex.Message });

            }

        }



    }
}
