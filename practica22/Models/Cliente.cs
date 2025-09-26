using System;

public class Cliente : Persona
{
    public string ReferenciaDireccion { get; private set; }

    public Cliente(string nombre, string direccion, string telefono, string referencia)
        : base(nombre, direccion, telefono)
    {
        ReferenciaDireccion = referencia;
    }
}