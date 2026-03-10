using DiakKurzusApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("")]
    public class DiakokController : ControllerBase
    {
        private readonly EgyetemContext _context;

        public DiakokController(EgyetemContext context)
        {
            _context = context;
        }

        // GET /diakLista
        [HttpGet("diakLista")]
        public async Task<ActionResult<IEnumerable<Diak>>> GetDiakok()
        {
            return await _context.Diakok.ToListAsync();
        }

        // GET /diakLista/{id} - opcionális, de hasznos lehet
        [HttpGet("diakLista/{id}")]
        public async Task<ActionResult<Diak>> GetDiak(int id)
        {
            var diak = await _context.Diakok.FindAsync(id);
            if (diak == null)
            {
                return NotFound();
            }
            return diak;
        }

        // POST /diakHozzaad
        [HttpPost("diakHozzaad")]
        public async Task<ActionResult> PostDiak(Diak diak)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Diakok.Add(diak);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                diak,
                message = "Az adatok sikeresen feltöltésre kerültek az adatbázisba."
            });
        }

        // PUT /diakFrissit/{id}
        [HttpPut("diakFrissit/{id}")]
        public async Task<IActionResult> PutDiak(int id, Diak diak)
        {
            if (id != diak.Id)
            {
                return BadRequest("Az URL-ben szereplő ID nem egyezik a küldött diák ID-jával.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(diak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiakExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(diak);
        }

        // DELETE /diakTorles/{id}
        [HttpDelete("diakTorles/{id}")]
        public async Task<IActionResult> DeleteDiak(int id)
        {
            var diak = await _context.Diakok.FindAsync(id);
            if (diak == null)
            {
                return NotFound();
            }

            _context.Diakok.Remove(diak);
            await _context.SaveChangesAsync();

            return Ok(new { message = "A diák sikeresen törölve." });
        }

        private bool DiakExists(int id)
        {
            return _context.Diakok.Any(e => e.Id == id);
        }
    }
}
