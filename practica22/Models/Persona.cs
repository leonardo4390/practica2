using System;
public abstract class Persona
{
    public string Nombre { get; protected set; }
    public string Direccion { get; protected set; }
    public string Telefono { get; protected set; }

    public Persona(string nombre, string direccion, string telefono)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}