using System;
using System.IO;
using System.Text.Json;

using System.IO;
using System.Text.Json;

public class AccesoADatosCadeteria
{
    private readonly string ruta = Path.Combine("Datos", "cadeteria.json");

    public CadeteriaBase Obtener()
    {
        if (!File.Exists(ruta))
            throw new FileNotFoundException($"No se encontró el archivo: {ruta}");

        var json = File.ReadAllText(ruta);

        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidDataException("El archivo cadeteria.json está vacío.");

        var datos = JsonSerializer.Deserialize<CadeteriaBase>(json);

        if (datos == null)
            throw new InvalidDataException("No se pudo deserializar el archivo cadeteria.json.");

        return datos;
    }
}