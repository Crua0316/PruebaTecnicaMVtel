using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class TransaccionesController : ControllerBase
{
    private readonly string filePath = "data.json"; // Ruta del archivo JSON

    // Método para leer el archivo JSON
    private List<Pedido> LeerPedidos()
    {
        if (!System.IO.File.Exists(filePath))
        {
            return new List<Pedido>();
        }

        var jsonData = System.IO.File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Pedido>>(jsonData) ?? new List<Pedido>();
    }

    // Método para escribir al archivo JSON
    private void GuardarPedidos(List<Pedido> pedidos)
    {
        var jsonData = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(filePath, jsonData);
    }

    // POST /transacciones: Agrega una nueva transacción
    [HttpPost("transacciones")]
    public IActionResult AgregarTransaccion([FromBody] Pedido nuevoPedido)
    {
        // Validar el formato de la transacción
        if (string.IsNullOrEmpty(nuevoPedido.Cliente) || nuevoPedido.Productos == null || !nuevoPedido.Productos.Any())
        {
            return BadRequest("El pedido no tiene un cliente válido o la lista de productos está vacía.");
        }

        if (nuevoPedido.Productos.Any(p => string.IsNullOrEmpty(p.Nombre) || p.Precio <= 0 || p.Cantidad <= 0))
        {
            return BadRequest("Uno o más productos tienen un formato inválido.");
        }

        // Leer pedidos existentes
        var pedidos = LeerPedidos();

        // Asignar ID y fecha
        nuevoPedido.Id = pedidos.Count + 1;
        nuevoPedido.Fecha = DateTime.Now;

        // Agregar el nuevo pedido y guardar
        pedidos.Add(nuevoPedido);
        GuardarPedidos(pedidos);

        return Ok("Transacción agregada exitosamente.");
    }

    // GET /resumen: Devuelve el resumen
    [HttpGet("resumen")]
    public IActionResult ObtenerResumen()
    {
        // Leer pedidos existentes
        var pedidos = LeerPedidos();

        if (!pedidos.Any())
        {
            return NotFound("No hay transacciones para generar el resumen.");
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

        var resumen = new
        {
            ClienteMayorGasto = clienteMayorGasto,
            ProductoMasVendido = productoMasVendido
        };

        return Ok(resumen);
    }
}
