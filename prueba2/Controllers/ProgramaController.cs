using Microsoft.AspNetCore.Mvc;

namespace prueba2.Controllers;

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
    // public ActionResult<TvProgram> BuscarPrograma([FromQuery] int id)
    // {
    //     var programa = _programas.BuscarPrograma(id);
    //     return Ok(programa);
    // }
    public ActionResult<List<TvProgram>> BuscarPrograma([FromQuery] DiaDeLaSemana dia)
    {
        var programas = _programas.BuscarPrograma(dia);

        if (programas == null || !programas.Any())
            return NotFound(new { status = 404, error = $"No hay programas para el día {dia}." });

        return Ok(programas);
    }


    [HttpPost("crearPrograma")]
    public IActionResult CrearPrograma([FromBody] TvProgram nuevoTvProgram)
    {
        var programa = new TvProgram(nuevoTvProgram.Id, nuevoTvProgram.Title, nuevoTvProgram.Genre,nuevoTvProgram.DiaDeLaSemana,nuevoTvProgram.StarTime, nuevoTvProgram.DurationMinutes );
        var agregado = _programas.CrearPrograma(programa);
        if (!agregado)
            return Conflict("Ya existe u horarios concidente, no creado");
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

    // [HttpPut("modificarPrograma")]
    // public IActionResult ModificarPrograma(int id, [FromBody] int nuevoDurationMinute)
    // {
    //     bool modificado = _programas.ModificarProgramaDuration(id, nuevoDurationMinute);
    //     if (!modificado)
    //         return NotFound("Programa no encontrado");

    //     return Ok("Duration Minute modificado");
    // }

    // [HttpPut("modificarPrograma")]
    // public IActionResult ModificarDuracionPorDia([FromQuery] DiaDeLaSemana dia, [FromQuery] int nuevaDuracion)
    // {
    //     try
    //     {
    //         var modificado = _programas.ModificarProgramaDuration(dia, nuevaDuracion);

    //         if (!modificado)
    //             return NotFound(new { status = 404, error = $"No se encontraron programas para el día {dia}." });

    //         return Ok(new { status = 200, message = $"Duración modificada correctamente para los programas del día {dia}." });
    //     }
    //     catch (ArgumentException ex)
    //     {
    //         return BadRequest(new { status = 400, error = ex.Message });
    //     }
    // }
    [HttpPut("modificarDuracion")]
    public IActionResult ModificarDuracion([FromQuery] int id, [FromQuery] int nuevaDuracion)
    {
        try
        {
            var modificado = _programas.ModificarProgramaDuration(id, nuevaDuracion);

            if (!modificado)
                return NotFound(new { status = 404, error = "No se encontró el programa con ese Id." });

            return Ok(new { status = 200, message = "Duración modificada correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { status = 400, error = ex.Message });
        }
    }
}
