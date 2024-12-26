using System.Text.Json.Serialization;

public class Pedido
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("cliente")]
    public string Cliente { get; set; } = string.Empty;

    [JsonPropertyName("productos")]
    public List<Producto> Productos { get; set; } = new List<Producto>();

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }
}


