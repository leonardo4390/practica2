public class CadeteriaBase
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    public string ObtenerNombre() => Nombre;
    public string ObtenerTelefono() => Telefono;
}