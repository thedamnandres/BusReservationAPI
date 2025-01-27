using BusReservationApi.Data;
using BusReservationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusReservationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoletosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BoletosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Obtener un boleto por su ReservaId
    [HttpGet("ver-boleto/{reservaId}")]
    public IActionResult VerBoleto(int reservaId)
    {
        var boleto = _context.Boletos.FirstOrDefault(b => b.ReservaId == reservaId);
        if (boleto == null)
        {
            return NotFound(new { mensaje = "Boleto no encontrado" });
        }

        return Ok(boleto);
    }

    [HttpGet("ver-boletos")]
    public IActionResult VerBoletos()
    {
        var boletos = _context.Boletos.ToList();
        if (!boletos.Any())
        {
            return NotFound(new { mensaje = "No hay boletos disponibles" });
        }

        return Ok(boletos);
    }
    
    // Crear un nuevo boleto con un ReservaId único
    [HttpPost("crear")]
    public async Task<IActionResult> CreateBoleto([FromBody] Boleto boleto)
    {
        if (boleto == null)
        {
            return BadRequest(new { mensaje = "Datos del boleto no son válidos" });
        }

        try
        {
            // Generar un ReservaId único
            int nuevoReservaId;
            do
            {
                nuevoReservaId = new Random().Next(1000, 9999); // Genera un número aleatorio entre 1000 y 9999
            } while (_context.Boletos.Any(b => b.ReservaId == nuevoReservaId));

            boleto.ReservaId = nuevoReservaId;

            _context.Boletos.Add(boleto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Boleto creado correctamente", boleto });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al guardar el boleto", error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    // Editar un boleto por su ReservaId
    [HttpPut("editar/{reservaId}")]
    public async Task<IActionResult> EditarBoleto(int reservaId, [FromBody] Boleto boletoActualizado)
    {
        if (boletoActualizado == null)
        {
            return BadRequest(new { mensaje = "Datos del boleto no son válidos" });
        }

        var boletoExistente = _context.Boletos.FirstOrDefault(b => b.ReservaId == reservaId);
        if (boletoExistente == null)
        {
            return NotFound(new { mensaje = "Boleto no encontrado" });
        }

        try
        {
            // Actualizar los campos
            boletoExistente.NombrePasajero = boletoActualizado.NombrePasajero;
            boletoExistente.Asiento = boletoActualizado.Asiento;
            boletoExistente.FechaViaje = boletoActualizado.FechaViaje;
            boletoExistente.Precio = boletoActualizado.Precio;
            boletoExistente.EstadoReserva = boletoActualizado.EstadoReserva;

            _context.Boletos.Update(boletoExistente);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Boleto actualizado correctamente", boleto = boletoExistente });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al actualizar el boleto", error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    // Eliminar un boleto por su ReservaId
    [HttpDelete("eliminar/{reservaId}")]
    public async Task<IActionResult> EliminarBoleto(int reservaId)
    {
        var boleto = _context.Boletos.FirstOrDefault(b => b.ReservaId == reservaId);
        if (boleto == null)
        {
            return NotFound(new { mensaje = "Boleto no encontrado" });
        }

        try
        {
            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Boleto eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al eliminar el boleto", error = ex.InnerException?.Message ?? ex.Message });
        }
    }   
}