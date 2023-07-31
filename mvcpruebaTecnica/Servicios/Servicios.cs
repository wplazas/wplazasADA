using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using mvcpruebaTecnica.Models;
using System.Text.Json.Nodes;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace mvcpruebaTecnica.Servicios
{
    public class Servicios
    {

        public Boolean crearUsuario(string nombres, string direccion,string telefono,string usuario,string identificacion,string contrasena,int idtipo)
        {
            Boolean pasa = true;
            var url = $"http://localhost:5280/api/Usuario/creausuario";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string json = $"{{\"nombres\":\"{nombres}\",\"direccion\":\"{direccion}\",\"telefono\":\"{telefono}\",\"usuario\":\"{usuario}\",\"identificacion\":\"{identificacion}\",\"contrasena\":\"{contrasena}\",\"idtipo\":{idtipo}}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) pasa=false ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            pasa = true;
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                pasa = false;
            }


            return pasa;
        }

        public string tipousuario(string usuario,string password)
        {
            UsuarioExiste jsonObj = new UsuarioExiste();
            //comodin indica el probable comportamiento de un usuario en el logueo:
            //si existe, si existe pero el password es erroneo, si no existe, el tipo de usuario, etc.
            string comodin = "";
            var url = $"http://localhost:5280/api/UsuarioExiste/existeusuario/"+usuario+"?password="+password;
            var request = (HttpWebRequest)WebRequest.Create(url);
            string json = $"{{\"usuario\":\"{usuario}\",\"password\":\"{password}\"}}";
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) comodin = "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            responseBody = responseBody.Replace("{\"usas\":[", "");
                            responseBody = responseBody.Replace("]}", "");

                            jsonObj = JsonConvert.DeserializeObject<UsuarioExiste>(responseBody);

                            var idtipo= jsonObj.idtipo;

                            if (jsonObj.existe.ToString() == "1")
                            {
                                if (jsonObj.sucess.ToString() == "1")
                                {
                                    comodin = jsonObj.idtipo.ToString();

                                }
                                else
                                {
                                    comodin = "password Erroneo";
                                }
                            }
                            else
                            {
                                comodin = "No Existe";
                            }

                            
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                comodin = "";
            }

            return comodin;
        }

        public List<Movimiento> movimiento()
        {
            List<Movimiento> lista = new List<Movimiento>();
            Movimiento mov = new Movimiento("","","","");
            //comodin indica el probable comportamiento de un usuario en el logueo:
            //si existe, si existe pero el password es erroneo, si no existe, el tipo de usuario, etc.
            string comodin = "";
            var url = $"http://localhost:5280/api/Movimiento/movimiento/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string respuesta = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) comodin = "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {

                           string responseBody = objReader.ReadToEnd();

                            dynamic responseBody_ =JsonConvert.DeserializeObject<dynamic>(responseBody);

                            foreach (JObject item in responseBody_) 
                            {
                                string _usuario = item.GetValue("usuario").ToString();
                                string _producto = item.GetValue("producto").ToString();
                                string _cantidadcompra = item.GetValue("cantidadcompra").ToString();
                                string _disponible= item.GetValue("disponible").ToString();
                                mov.usuario = _usuario;
                                mov.producto = _producto;
                                mov.cantidadcompra = _cantidadcompra;
                                mov.disponible = _disponible;
                                lista.Add(new Movimiento(_usuario, _producto, _cantidadcompra, _disponible));

                            }


                        }
                    }
                }
            }
            catch (WebException ex)
            {
                lista =null;
            }

            return lista;
        }



        public List<Productos> listaproductos()
        {
            List<Productos> lista = new List<Productos>();
            Productos prod = new Productos(1,"",1,"");
            //comodin indica el probable comportamiento de un usuario en el logueo:
            //si existe, si existe pero el password es erroneo, si no existe, el tipo de usuario, etc.
            string comodin = "";
            var url = $"http://localhost:5280/api/Producto/listado/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string respuesta = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) comodin = "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {

                            string responseBody = objReader.ReadToEnd();

                            dynamic responseBody_ = JsonConvert.DeserializeObject<dynamic>(responseBody);

                            foreach (JObject item in responseBody_) // 
                            {
                                int _id = ((int)item.GetValue("id"));
                                string _nombre  = item.GetValue("nombre").ToString();
                                int _disponible = (int)item.GetValue("disponible");
                                string _descripcion = item.GetValue("descripcion").ToString();
                                prod.id = _id;
                                prod.nombre = _nombre;
                                prod.disponible = _disponible;
                                prod.descripcion = _descripcion;
                                lista.Add(new Productos(_id,_nombre, _disponible, _descripcion));

                            }


                        }
                    }
                }
            }
            catch (WebException ex)
            {
                lista = null;
            }

            return lista;
        }


    }
}
