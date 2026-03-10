using DiakKurzusApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("")]
    public class KurzusokController : ControllerBase
    {
        private readonly EgyetemContext _context;

        public KurzusokController(EgyetemContext context)
        {
            _context = context;
        }

        // GET /kurzusLista
        [HttpGet("kurzusLista")]
        public async Task<ActionResult<IEnumerable<Kurzus>>> GetKurzusok()
        {
            return await _context.Kurzusok.ToListAsync();
        }

        // GET /kurzusLista/{id} - opcionális
        [HttpGet("kurzusLista/{id}")]
        public async Task<ActionResult<Kurzus>> GetKurzus(int id)
        {
            var kurzus = await _context.Kurzusok.FindAsync(id);
            if (kurzus == null)
            {
                return NotFound();
            }
            return kurzus;
        }

        // POST /kurzusHozzaad
        [HttpPost("kurzusHozzaad")]
        public async Task<ActionResult> PostKurzus(Kurzus kurzus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Kurzusok.Add(kurzus);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                kurzus,
                message = "Az adatok sikeresen feltöltésre kerültek az adatbázisba."
            });
        }

        // PUT /kurzusFrissit/{id}
        [HttpPut("kurzusFrissit/{id}")]
        public async Task<IActionResult> PutKurzus(int id, Kurzus kurzus)
        {
            if (id != kurzus.Id)
            {
                return BadRequest("Az URL-ben szereplő ID nem egyezik a küldött kurzus ID-jával.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(kurzus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KurzusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(kurzus);
        }

        // DELETE /kurzusTorles/{id}
        [HttpDelete("kurzusTorles/{id}")]
        public async Task<IActionResult> DeleteKurzus(int id)
        {
            var kurzus = await _context.Kurzusok.FindAsync(id);
            if (kurzus == null)
            {
                return NotFound();
            }

            _context.Kurzusok.Remove(kurzus);
            await _context.SaveChangesAsync();

            return Ok(new { message = "A kurzus sikeresen törölve." });
        }

        private bool KurzusExists(int id)
        {
            return _context.Kurzusok.Any(e => e.Id == id);
        }
    }
}
