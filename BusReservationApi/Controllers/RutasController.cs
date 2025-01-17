using BusReservationApi.Data;
using BusReservationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusReservationApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RutasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RutasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ruta>>> GetRutas()
    {
        return await _context.Rutas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ruta>> GetRuta(int id)
    {
        var ruta = await _context.Rutas.FindAsync(id);
        if (ruta == null)
        {
            return NotFound();
        }

        return ruta;
    }

    [HttpPost]
    public async Task<ActionResult<Ruta>> PostRuta(Ruta ruta)
    {
        _context.Rutas.Add(ruta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRuta), new { id = ruta.IdRuta }, ruta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRuta(int id, Ruta ruta)
    {
        if (id != ruta.IdRuta)
        {
            return BadRequest();
        }

        _context.Entry(ruta).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rutas.Any(e => e.IdRuta == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRuta(int id)
    {
        var ruta = await _context.Rutas.FindAsync(id);
        if (ruta == null)
        {
            return NotFound();
        }

        _context.Rutas.Remove(ruta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}