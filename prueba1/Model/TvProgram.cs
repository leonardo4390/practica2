using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiaDeLaSemana
{Lunes,Martes,Miercoles,Jueves,Viernes,Sabado,Domingo}

public class TvProgram
{
    public int Id{get;private set;}
    // REGLA DE NEGOCIO PARA Title
    private string title;
    public string Title
    {
        get => title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El título no puede estar vacío.");
            if (value.Length > 50)
                throw new ArgumentException("El título no puede superar los 50 caracteres.");
            title = value;
        }
    }
    //public string Title{get;private set;}
    //REGLA DE NEGOCIO PARA Genre
    private string genre;
    public string Genre
    {
        get => genre;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El género no puede estar vacío.");
            if (value.Length > 50)
                throw new ArgumentException("El género no puede superar los 50 caracteres.");
            genre = value;
        }
    }
    //public string Genre{get;private set;}
    public DiaDeLaSemana DiaDeLaSemana{get;set;}
    public DateTime StarTime{get;set;}
    //REGLA DE NEGOCIO PARA DurationMinutes
    private int durationMinutes;
    public int DurationMinutes
    {
        get => durationMinutes;
        set
        {
            if (value != 30 && value != 60 && value != 90 && value != 120 && value != 45)
                throw new ArgumentException("Duración inválida. Solo se permiten 30,45, 60, 90 o 120 minutos.");
            durationMinutes = value;
        }
    }
    //public int DurationMinutes{get;set;}

    public TvProgram(int Id, string Title, string Genre, DiaDeLaSemana DiaDeLaSemana, DateTime StarTime, int DurationMinutes)
    {
        this.Id = Id;
        this.Title = Title;
        this.Genre = Genre;
        this.DiaDeLaSemana = DiaDeLaSemana;
        this.StarTime = StarTime;
        this.DurationMinutes = DurationMinutes;
    }
}

// OTRAS VALIDACIONES POSIBLES:

/*- Horario dentro de un rango permitido
- Ejemplo: un programa no puede empezar antes de las 06:00 ni después de las 23:00.
- Regla:

if (StartTime.Hour < 6 || StartTime.Hour > 23)
    throw new ArgumentException("El programa debe emitirse entre las 06:00 y las 23:00.");
*/

/*
- No permitir programas sin duración
- DurationMinutes nunca puede ser 0.
- Evita programas “fantasma”.

private int durationMinutes;
public int DurationMinutes
{
    get => durationMinutes;
    set
    {
        if (value <= 0)
            throw new ArgumentException("La duración debe ser mayor a 0 minutos.");
        
        if (!DuracionesValidas.Contains(value))
            throw new ArgumentException(
                $"Duración inválida: {value}. Solo se permiten {string.Join(", ", DuracionesValidas)} minutos."
            );

        durationMinutes = value;
    }
}

*/

/*
- Restricción de género según horario (ejemplo didáctico)
- Programas infantiles solo entre 08:00 y 20:00.
- Programas de adultos después de las 22:00.
- Esto simula reglas de contenido según franja horaria.

private string genre;
public string Genre
{
    get => genre;
    private set
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El género no puede estar vacío.");
        if (value.Length > 50)
            throw new ArgumentException("El género no puede superar los 50 caracteres.");

        // Validación de franja horaria según género
        if (value.Equals("Infantil", StringComparison.OrdinalIgnoreCase))
        {
            if (StartTime.Hour < 8 || StartTime.Hour >= 20)
                throw new ArgumentException("Los programas infantiles solo pueden emitirse entre las 08:00 y las 20:00.");
        }

        if (value.Equals("Adultos", StringComparison.OrdinalIgnoreCase))
        {
            if (StartTime.Hour < 22)
                throw new ArgumentException("Los programas para adultos solo pueden emitirse después de las 22:00.");
        }

        genre = value;
    }
}
*/

/*
UNICIDAD DE PROGRAMAS EN UN MISMO DIA
se lo hace en repositorio en este caso en Programas
public enum ResultadoCreacion
{
    Exito,
    YaExiste,
    ChoqueHorario,
    TituloDuplicado
}

public ResultadoCreacion CrearPrograma(TvProgram nuevoPrograma)
{
    // Validar si ya existe por Id
    if (_tvProgram.Any(t => t.Id == nuevoPrograma.Id))
        return ResultadoCreacion.YaExiste;

    // Validar unicidad de título por día
    bool tituloDuplicado = _tvProgram.Any(p =>
        p.DiaDeLaSemana == nuevoPrograma.DiaDeLaSemana &&
        p.Title.Equals(nuevoPrograma.Title, StringComparison.OrdinalIgnoreCase)
    );

    if (tituloDuplicado)
        return ResultadoCreacion.TituloDuplicado;

    // Validar choque de horarios
    DateTime nuevoFin = nuevoPrograma.StartTime.AddMinutes(nuevoPrograma.DurationMinutes);
    bool choque = _tvProgram.Any(p =>
        p.DiaDeLaSemana == nuevoPrograma.DiaDeLaSemana &&
        nuevoPrograma.StartTime < p.StartTime.AddMinutes(p.DurationMinutes) &&
        nuevoFin > p.StartTime
    );

    if (choque)
        return ResultadoCreacion.ChoqueHorario;

    // Autoincrementar Id
    int nuevoId = _tvProgram.Any() ? _tvProgram.Max(t => t.Id) + 1 : 1;
    nuevoPrograma = new TvProgram(
        nuevoId,
        nuevoPrograma.Title,
        nuevoPrograma.Genre,
        nuevoPrograma.DiaDeLaSemana,
        nuevoPrograma.StartTime,
        nuevoPrograma.DurationMinutes
    );

    _tvProgram.Add(nuevoPrograma);
    _accesoADatos.Guardar(_tvProgram);
    return ResultadoCreacion.Exito;
}
*/

/*
DURACIONES MAXIMAS Y MINIMAS DE PROGRAMAS => se lo hace en el setter
La validación depende del género. Se hace en el setter de DurationMinutes o en el constructor, 
porque ahí tenés acceso tanto al género como a la duración.

private int durationMinutes;
public int DurationMinutes
{
    get => durationMinutes;
    set
    {
        if (value <= 0)
            throw new ArgumentException("La duración debe ser mayor a 0 minutos.");

        // Validación según género
        if (Genre.Equals("Noticias", StringComparison.OrdinalIgnoreCase))
        {
            if (value < 30 || value > 60)
                throw new ArgumentException("Las noticias deben durar entre 30 y 60 minutos.");
        }

        if (Genre.Equals("Peliculas", StringComparison.OrdinalIgnoreCase))
        {
            if (value < 90)
                throw new ArgumentException("Las películas deben durar al menos 90 minutos.");
        }

        durationMinutes = value;
    }
}
*/

/*
VALIDACION DE FECHAS COHERENTES
Se hace en el constructor, porque ahí se asigna StartTime. La idea es que no se pueda crear un
 programa en una fecha pasada.

 public TvProgram(int id, string title, string genre, DiaDeLaSemana diaDeLaSemana, DateTime startTime, int durationMinutes)
{
    if (startTime < DateTime.Now)
        throw new ArgumentException("La fecha/hora de inicio no puede estar en el pasado.");

    Id = id;
    Title = title;
    Genre = genre;
    DiaDeLaSemana = diaDeLaSemana;
    StartTime = startTime;
    DurationMinutes = durationMinutes; // usa el setter con validación
}

*/

/*

*/
