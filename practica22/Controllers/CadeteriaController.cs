using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly Cadeteria _cadeteria;

    public CadeteriaController(Cadeteria cadeteria)
    {
        _cadeteria = cadeteria;
    }

    [HttpGet("pedidos")]
    public ActionResult<List<string>> GetPedidos()
    {
        return _cadeteria.ListarPedidos();
    }

    [HttpGet("cadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        return _cadeteria.Cadetes;
    }

    [HttpGet("informe")]
    public ActionResult<List<string>> GetInforme()
    {
        return _cadeteria.ObtenerInforme();
    }

    [HttpPost("pedido")]
    public IActionResult AgregarPedido([FromBody] PedidoDTO dto)
    {
        var productos = dto.Productos.Select(p => new Producto(p)).ToList();
        _cadeteria.CrearPedido(dto.Nro, dto.Obs, dto.NombreCliente, dto.Direccion, dto.Telefono, dto.Referencia, productos);
        return Ok("Pedido agregado");
    }

    [HttpPut("asignar")]
    public IActionResult AsignarPedido(int idPedido, int idCadete)
    {
        bool aceptado = _cadeteria.AsignarCadeteAPedido(idCadete, idPedido);
        return aceptado ? Ok("Pedido asignado") : NotFound("Pedido o cadete no encontrado");
    }

    [HttpPut("estado")]
    public IActionResult CambiarEstadoPedido(int idPedido, string nuevoEstado)
    {
        bool aceptado = _cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
        return aceptado ? Ok("Estado actualizado") : NotFound("Pedido no encontrado");
    }

    [HttpPut("cambiarcadete")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var pedido = _cadeteria.ObtenerPedidoPorNumero(idPedido);
        var nuevoCadete = _cadeteria.ObtenerCadetePorId(idNuevoCadete);

        if (pedido == null || nuevoCadete == null)
            return NotFound("Pedido o cadete no encontrado");

        pedido.AsignarCadete(nuevoCadete);
        return Ok("Cadete actualizado");
    }
}