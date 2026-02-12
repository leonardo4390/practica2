using System;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiaDeLaSemana
{Lunes,Martes,Miercoles,Jueves,Viernes, Sabado,Domingo}

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
            if (value.Length > 100)
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


    public TvProgram(int id, string title, string genre,DiaDeLaSemana diaDeLaSemana, DateTime starTime, int durationMinutes)
    {
        Id = id;
        Title = title;
        Genre = genre;
        DiaDeLaSemana = diaDeLaSemana;
        StarTime = starTime;
        DurationMinutes = durationMinutes;   
    }
}

