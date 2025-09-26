public class PedidoDTO
{
    public int Nro { get; set; }
    public string Obs { get; set; }
    public string NombreCliente { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Referencia { get; set; }
    public List<Producto.Comida> Productos { get; set; }
}