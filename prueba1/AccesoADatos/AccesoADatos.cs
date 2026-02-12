using System;
using System.Text.Json;

public class AccesoADatos: IAccesoADatos<TvProgram>
{
    private readonly string ruta = Path.Combine("Data", "data.json");
    /*
     public List<TvProgram> Obtener()
    {
        // Si no existe el archivo, lo creo vacío
        if (!File.Exists(ruta))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);
            File.WriteAllText(ruta, "[]");
        }

        var json = File.ReadAllText(ruta);

        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() } // enum como string
        };

        return JsonSerializer.Deserialize<List<TvProgram>>(json, opciones) ?? new List<TvProgram>();
    }

    public void Guardar(List<TvProgram> datos)
    {
        var directorio = Path.GetDirectoryName(ruta);
        if (!Directory.Exists(directorio))
            Directory.CreateDirectory(directorio!);

        var opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() } // enum como string
        };

        var json = JsonSerializer.Serialize(datos, opciones);
        File.WriteAllText(ruta, json);
    }
    */
    public List<TvProgram> Obtener()
    {
        var json = File.ReadAllText(ruta);
        if (!File.Exists(ruta))
            File.WriteAllText(ruta, "[]");

        var opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<List<TvProgram>>(json, opciones) ?? new List<TvProgram>();
    }

    public void Guardar(List<TvProgram> datos)
    {
        var directorio = Path.GetDirectoryName(ruta);
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(directorio);

        var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
    }

    public bool Eliminar(int id)
    {
        var programa = Obtener();
        int eliminado = programa.RemoveAll(p => p.Id == id);
        if (eliminado > 0)
        {
            Guardar(programa);
            return true;
        }
        return false;
    }
}