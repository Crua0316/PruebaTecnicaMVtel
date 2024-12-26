using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    public class Producto
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
    }

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

    static void Main()
    {
        string filePath = "data.json"; // Ruta del archivo JSON
        try
        {
            var jsonData = File.ReadAllText(filePath);
            var pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonData);

            if (pedidos == null || !pedidos.Any())
            {
                Console.WriteLine("El archivo JSON no contiene datos válidos o está vacío.");
                return;
            }

            // Cliente con mayor gasto total
            var clienteMayorGasto = pedidos
                .GroupBy(p => p.Cliente)
                .Select(g => new
                {
                    Cliente = g.Key,
                    TotalGasto = g.Sum(p => p.Productos.Sum(prod => prod.Precio * prod.Cantidad))
                })
                .OrderByDescending(c => c.TotalGasto)
                .FirstOrDefault();

            // Producto más vendido
            var productoMasVendido = pedidos
                .SelectMany(p => p.Productos)
                .GroupBy(prod => prod.Nombre)
                .Select(g => new
                {
                    Producto = g.Key,
                    TotalCantidad = g.Sum(prod => prod.Cantidad)
                })
                .OrderByDescending(p => p.TotalCantidad)
                .FirstOrDefault();

            // Mostrar resultados
            Console.WriteLine("\nResumen:");
            if (clienteMayorGasto != null)
            {
                Console.WriteLine($"Cliente con mayor gasto total: {clienteMayorGasto.Cliente} (${clienteMayorGasto.TotalGasto:F2})");
            }
            else
            {
                Console.WriteLine("No se pudo determinar el cliente con mayor gasto.");
            }

            if (productoMasVendido != null)
            {
                Console.WriteLine($"Producto más vendido: {productoMasVendido.Producto} (Cantidad: {productoMasVendido.TotalCantidad})");
            }
            else
            {
                Console.WriteLine("No se pudo determinar el producto más vendido.");
            }

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: El archivo no se encontró");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error: El archivo JSON está mal formateado. Detalles: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
    }
}
