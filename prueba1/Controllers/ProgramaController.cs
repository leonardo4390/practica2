using Microsoft.AspNetCore.Mvc;

namespace prueba1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramaController : ControllerBase
{
    private readonly Programas _programas;
    public ProgramaController()
    {
        IAccesoADatos<TvProgram> accesoDatos = new AccesoADatos();
        _programas = new Programas(accesoDatos);
    }

    [HttpGet("listarProgramas")]

    public ActionResult<List<string>> ListarProgramas()
    {
        var programas = _programas.ListarProgramas();
        return Ok(programas);
    }

    [HttpGet("buscar")]
    public ActionResult<TvProgram> BuscarPrograma([FromQuery] int id)
    {
        var programa = _programas.BuscarPrograma(id);
        return Ok(programa);
    }

    [HttpPost("crearPrograma")]
    public IActionResult AgregarCliente([FromBody] TvProgram nuevoTvProgram)
    {
        var programa = new TvProgram(nuevoTvProgram.Id, nuevoTvProgram.Title, nuevoTvProgram.Genre,nuevoTvProgram.DiaDeLaSemana,nuevoTvProgram.StarTime, nuevoTvProgram.DurationMinutes );
        var agregado = _programas.CrearPrograma(programa);
        if (!agregado)
            return Conflict("Ya existe, no creado");
        return Ok("programa creado");
    }

    [HttpDelete("eliminar")]
    public IActionResult EliminarPrograma(int id)
    {
        var programa = _programas.EliminarPrograma(id);
        if (!programa)
            return NotFound("No se encontro programa");
        return Ok("programa eliminado");
    }

    [HttpPut("modificarPrograma")]
    public IActionResult ModificarPrograma(int id, [FromBody] int nuevoDurationMinute)
    {
        bool modificado = _programas.ModificarProgramaDuration(id, nuevoDurationMinute);
        if (!modificado)
            return NotFound("Programa no encontrado");

        return Ok("Duration Minute modificado");
    }
}
