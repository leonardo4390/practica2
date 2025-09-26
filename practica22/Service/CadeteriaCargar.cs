using System;

public static class CadeteriaCargar
{
    public static Cadeteria CrearInstancia()
    {
        var accesoCadeteria = new AccesoADatosCadeteria();
        var accesoCadetes = new AccesoADatosCadetes();
        var accesoPedidos = new AccesoADatosPedidos();

        return new Cadeteria(accesoCadeteria, accesoCadetes, accesoPedidos);
    }
}