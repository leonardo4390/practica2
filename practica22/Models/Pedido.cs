using System;
public class Pedido
{
    public int Nro { get; private set; }
    public string Obs { get; private set; }
    public Cliente Datocliente { get; private set; }
    public string Estado { get; set; }
    public List<Producto> Productos { get; private set; } = new List<Producto>();
    public Cadete CadeteAsignado { get; private set; }
    public Pedido() { }

    public Pedido(int nro, string obs, Cliente cliente, string estado)
    {
        Nro = nro;
        Obs = obs;
        Datocliente = cliente;
        Estado = estado;
        Productos = new List<Producto>();
    }

    public void AsignarCadete(Cadete cadete) => CadeteAsignado = cadete;

    public void AgregarProducto(Producto producto) => Productos.Add(producto);

    public bool EstaEntregado() => Estado.ToLower() == "entregado";

    public List<string> ObtenerResumen()
    {
        var resumen = new List<string>
        {
            $"Pedido N°: {Nro}",
            $"Cliente: {Datocliente.Nombre} - Tel: {Datocliente.Telefono}",
            $"Dirección: {Datocliente.Direccion} - Ref: {Datocliente.ReferenciaDireccion}",
            $"Observación: {Obs}",
            $"Estado: {Estado}",
            $"Cadete asignado: {(CadeteAsignado != null ? CadeteAsignado.Nombre : "Sin asignar")}"
        };

        resumen.Add("Productos:");
        resumen.AddRange(Productos.Select(p => $"- {p}"));
        return resumen;
    }
}