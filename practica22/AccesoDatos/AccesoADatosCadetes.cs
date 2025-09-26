using System;
using System.Text.Json;

public class AccesoADatosCadetes
{
    private readonly string ruta = Path.Combine("Datos", "cadetes.json");

    public List<Cadete> Obtener()
    {
        var json = File.ReadAllText(ruta);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
    }
}