using Newtonsoft.Json;

namespace mvcpruebaTecnica.Models
{
    public class Productos
    {
        public Productos(int id, string nombre, int disponible, string descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.disponible = disponible;
            this.descripcion = descripcion;
        }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [JsonProperty("disponible")]
        public int disponible { get; set; }
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
    }
}
