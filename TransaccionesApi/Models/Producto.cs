using System.Text.Json.Serialization;

public class Producto
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("precio")]
    public decimal Precio { get; set; }

    [JsonPropertyName("cantidad")]
    public int Cantidad { get; set; }
}