using System;

public class Producto
{
    public enum Comida
    {
        Hamburguesa, Pizza, Empanada, Panchos, PapasFritas, Sandwich
    }

    public Comida Tipo { get; private set; }

    public Producto(Comida tipo)
    {
        Tipo = tipo;
    }

    public override string ToString() => Tipo.ToString();
}