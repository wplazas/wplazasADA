using Newtonsoft.Json;

namespace mvcpruebaTecnica.Models
{
    
    public class Movimiento
    {
        public Movimiento(string usuario, string producto, string cantidadcompra, string disponible)
        {
            this.usuario = usuario;
            this.producto = producto;
            this.cantidadcompra = cantidadcompra;
            this.disponible = disponible;
        }

        [JsonProperty("usuario")]
        public string usuario { get; set; }

        [JsonProperty("producto")]
        public string producto { get; set; }

        [JsonProperty("cantidadcompra")]
        public string cantidadcompra { get; set; }

        [JsonProperty("disponible")]
        public string disponible { get; set; }

    }
}
