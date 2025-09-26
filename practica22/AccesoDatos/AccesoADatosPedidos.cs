using System;
using System.Text.Json;

public class AccesoADatosPedidos
{
    private readonly string ruta = Path.Combine("Datos", "pedidos.json");

    public List<Pedido> Obtener()
    {
        if (!File.Exists(ruta)) return new List<Pedido>();
        var json = File.ReadAllText(ruta);
        return string.IsNullOrWhiteSpace(json)
            ? new List<Pedido>()
            : JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public void Guardar(List<Pedido> pedidos)
    {
        var json = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
    }
}