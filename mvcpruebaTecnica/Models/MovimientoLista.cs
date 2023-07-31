using Newtonsoft.Json;

namespace mvcpruebaTecnica.Models
{
    public class MovimientoLista
    {
            [JsonProperty("body")]
            public List<Movimiento> movim { get; set; }
        }
}

