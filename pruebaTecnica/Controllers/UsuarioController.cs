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
    public class UsuarioController : ControllerBase
    {
        private readonly string conexion;

        public UsuarioController(IConfiguration cadcon)
        {
            conexion = cadcon.GetConnectionString("myconexion");
        }

        [HttpGet]
        [Route("listado")]
        public IActionResult listado()
        {
            List<Usuario> usas = new List<Usuario>();
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("pa_listausuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usas.Add(new Usuario(){
                                id = reader.GetInt16("id"),
                                nombres=reader.GetString("nombres"),
                                direccion= reader.GetString("direccion"),
                                telefono = reader.GetString("telefono"),
                                usuario = reader.GetString("usuario"),
                                identificacion= reader.GetString("identificacion"),
                                contrasena= reader.GetString("contrasena"),
                                idtipo = reader.GetInt16("idtipo")
                            });
                        }
                    }
                }
                return StatusCode(statusCode: 200, new { mensaje = "ok", response = usas }) ;


            }catch(Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message, response = usas });

            }

        }


        [HttpPost]
        [Route("creausuario")]
        public IActionResult creausuario([FromBody] Usuario entydad)
        {
            try
            {
                using (var conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    var cmd = new SqlCommand("pa_crearusuario", conn);
                    cmd.Parameters.AddWithValue("nombres", entydad.nombres);
                    cmd.Parameters.AddWithValue("direccion", entydad.direccion);
                    cmd.Parameters.AddWithValue("telefono", entydad.telefono);
                    cmd.Parameters.AddWithValue("usuario", entydad.usuario);
                    cmd.Parameters.AddWithValue("identificacion", entydad.identificacion);
                    cmd.Parameters.AddWithValue("contrasena", entydad.contrasena);
                    cmd.Parameters.AddWithValue("idtipo", entydad.idtipo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(statusCode: 200, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(statusCode: 500, new { mensaje = ex.Message });

            }

        }

    }




}
