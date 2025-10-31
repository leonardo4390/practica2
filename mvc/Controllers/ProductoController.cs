using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;



public class ProductoController : Controller
{
    private static List<Producto> productos = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Café", Precio = 1200, Categoria = "Bebidas" },
        new Producto { Id = 2, Nombre = "Galletitas", Precio = 800, Categoria = "Snacks" },
        new Producto { Id = 3, Nombre = "Yerba Mate", Precio = 1500, Categoria = "Infusiones" }
    };

    // Muestra todos los productos
    public IActionResult Index()
    {
        return View(productos);
    }

    // Muestra un producto por id
    public IActionResult Detalle(int id)
    {
        var producto = productos.FirstOrDefault(p => p.Id == id);
        if (producto == null)
            return NotFound("Producto no encontrado");

        return View(producto);
    }
}
