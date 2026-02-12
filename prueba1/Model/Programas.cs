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
        /*bool existe = _tvProgram.Any(t =>t.Id == nuevoPrograma.Id);
        if (existe) return false;*/
        // CREANDO PROGRAMA SIN QUE CHOQUE LOS HORARIOS SI ES QUE ESTAN EN EL MISMO DIA
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

    public TvProgram BuscarPrograma(int id) =>
        _tvProgram.Find(t => t.Id == id);


    public bool ModificarProgramaDuration(int id, int nuevoDurationMinute)
    {
        var programa = BuscarPrograma(id);
        if (programa == null) return false;
        programa.DurationMinutes = nuevoDurationMinute;
        _accesoADatos.Guardar(_tvProgram);
        return true;
    }

    public bool EliminarPrograma(int id)
    {
        return _accesoADatos.Eliminar(id);

    }
}