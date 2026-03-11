using FilmLista.Data;
using FilmLista.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmLista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmekController : ControllerBase
    {
        private readonly FilmContext _context;

        public FilmekController(FilmContext context)
        {
            _context = context;
        }

        // GET: /filmLista
        [HttpGet("filmLista")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilmek()
        {
            return await _context.Filmek.ToListAsync();
        }

        // POST: /filmHozzaad
        [HttpPost("filmHozzaad")]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            // Egyszerű validáció – a modell állapot automatikusan ellenőrzi a [Required] attribútumokat
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Filmek.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, film);
        }

        // Segéd GET egyetlen filmhez (a CreatedAtAction miatt)
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _context.Filmek.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return film;
        }

        // PUT: /filmFrissit/{id}
        [HttpPut("filmFrissit/{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest("Az útvonalban lévő ID nem egyezik a film ID-jával.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(film); // Visszaadjuk a frissített objektumot
        }

        // DELETE: /filmTorles/{id}
        [HttpDelete("filmTorles/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Filmek.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Filmek.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent(); // Sikeres törlés
        }

        private bool FilmExists(int id)
        {
            return _context.Filmek.Any(e => e.Id == id);
        }
    }
}
