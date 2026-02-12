using System;
using System.Text.Json;


public class Programas
{
    private List<TvProgram> _tvProgram = new();
    private IAccesoADatos<TvProgram> _accesoADatos;

    public Programas(IAccesoADatos<TvProgram> accesoADatos)
    {
        _accesoADatos = accesoADatos;
        _tvProgram = _accesoADatos.Obtener();
    }

    public string ListarProgramas()
    {
        var opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(_tvProgram, opciones);
    }

    public bool CrearPrograma(TvProgram nuevoPrograma)
    {

            // Calcular fin del nuevo programa
        DateTime nuevoFin = nuevoPrograma.StarTime.AddMinutes(nuevoPrograma.DurationMinutes);

        // Validar que no se superponga con otro programa del mismo día
        bool choque = _tvProgram.Any(p =>
            //compara dia a dia
            p.DiaDeLaSemana == nuevoPrograma.DiaDeLaSemana &&
            // calcula la hora fin del programa existente
            nuevoPrograma.StarTime < p.StarTime.AddMinutes(p.DurationMinutes) &&
            // hora fin del nuevo programa
            nuevoFin > p.StarTime
        );

        if (choque)
            return false; // o lanzar excepción

        // Calcular el próximo Id Automaticamente
        int nuevoId = _tvProgram.Any() ? _tvProgram.Max(t => t.Id) + 1 : 1;
        // Id tiene private set, necesitas un constructor que lo reciba
        nuevoPrograma = new TvProgram(
        nuevoId,
        nuevoPrograma.Title,
        nuevoPrograma.Genre,
        nuevoPrograma.DiaDeLaSemana,
        nuevoPrograma.StarTime,
        nuevoPrograma.DurationMinutes
        );

        _tvProgram.Add(nuevoPrograma);
        _accesoADatos.Guardar(_tvProgram);
        return true;
    }

    // public TvProgram BuscarPrograma(int id) =>
    //     _tvProgram.Find(t => t.Id == id);

    public List<TvProgram> BuscarPrograma(DiaDeLaSemana dia)
    {
        return _tvProgram
            .Where(p => p.DiaDeLaSemana == dia)
            .ToList();
    }



    // public bool ModificarProgramaDuration(int id, int nuevoDurationMinute)
    // {
    //     var programa = BuscarPrograma(id);
    //     if (programa == null) return false;
    //     programa.DurationMinutes = nuevoDurationMinute;
    //     _accesoADatos.Guardar(_tvProgram);
    //     return true;
    // }

    // public bool ModificarProgramaDuration(DiaDeLaSemana dia, int nuevaDuracion)
    // {
    //     var programas = _tvProgram.Where(p => p.DiaDeLaSemana == dia).ToList();

    //     if (!programas.Any()) return false; // no hay programas ese día

    //     foreach (var programa in programas)
    //     {
    //         programa.DurationMinutes = nuevaDuracion; // aplica validaciones del modelo
    //     }

    //     _accesoADatos.Guardar(_tvProgram);
    //     return true;
    // }

    public bool ModificarProgramaDuration(int id, int nuevoDurationMinute)
    {
        var programa = _tvProgram.Find(p => p.Id == id);
        if (programa == null) return false;

        programa.DurationMinutes = nuevoDurationMinute; // aplica validaciones del modelo
        _accesoADatos.Guardar(_tvProgram);
        return true;
    }



    public bool EliminarPrograma(int id)
    {
        return _accesoADatos.Eliminar(id);

    }
}