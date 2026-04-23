using Konyvkatalogus.Data;
using Konyvkatalogus.DTOs;
using Konyvkatalogus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvKatalogus.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class KonyvController : ControllerBase
    {
        private readonly KonyvContext _context;

        public KonyvController(KonyvContext context)
        {
            _context = context;
        }

        /// Új könyv hozzáadása a katalógushoz
        /// <returns>A létrehozott könyv adatai és sikeres üzenet</returns>
        [HttpPost("adatHozzad")]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> AdatHozzad([FromBody] KonyvLetrehozasDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var konyv = new Konyv
            {
                Cim = dto.Cim.Trim(),
                KiadasIdeje = dto.KiadasIdeje,
                Szerzo = dto.Szerzo.Trim(),
                Kategoria = dto.Kategoria.Trim(),
                Isbn = dto.Isbn.Trim()
            };

            _context.Konyv.Add(konyv);
            await _context.SaveChangesAsync();

            var valasz = MapToValaszDto(konyv);

            return StatusCode(201, new
            {
                uzenet = "Az adatok sikeresen feltöltésre kerültek az adatbázisba",
                adat = valasz
            });
        }

        /// Összes könyv lekérdezése JSON formátumban
        /// <returns>A könyvek listája (Id, Cim, KiadasIdeje, Szerzo, Kategoria, Isbn)</returns>
        [HttpGet("adatKiir")]
        [ProducesResponseType(typeof(List<KonyvValaszDto>), 200)]
        public async Task<IActionResult> AdatKiir()
        {
            var konyvek = await _context.Konyv
                .OrderBy(k => k.Id)
                .Select(k => MapToValaszDto(k))
                .ToListAsync();

            return Ok(konyvek);
        }

        /// Egy könyv lekérdezése az Id alapján
        [HttpGet("adatKiir/{id:int}")]
        [ProducesResponseType(typeof(KonyvValaszDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AdatKiirEgy(int id)
        {
            var konyv = await _context.Konyv.FindAsync(id);

            if (konyv is null)
                return NotFound(new { uzenet = $"Nem található könyv {id} azonosítóval." });

            return Ok(MapToValaszDto(konyv));
        }

        /// Könyv adatainak teljes frissítése (Cim, Szerzo, Kategoria, Isbn)
        [HttpPut("adatFrissit/{id:int}")]
        [ProducesResponseType(typeof(KonyvValaszDto), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AdatFrissit(int id, [FromBody] KonyvFrissitesDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var konyv = await _context.Konyv.FindAsync(id);

            if (konyv is null)
                return NotFound(new { uzenet = $"Nem található könyv {id} azonosítóval." });

            konyv.Cim = dto.Cim.Trim();
            konyv.Szerzo = dto.Szerzo.Trim();
            konyv.Kategoria = dto.Kategoria.Trim();
            konyv.Isbn = dto.Isbn.Trim();

            await _context.SaveChangesAsync();

            return Ok(MapToValaszDto(konyv));
        }

        /// Könyv adatainak részleges frissítése
        [HttpPatch("adatFrissit/{id:int}")]
        [ProducesResponseType(typeof(KonyvValaszDto), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AdatReszlegesFrissit(int id, [FromBody] KonyvFrissitesDto dto)
        {
            // Azonos logika a PUT-tal, de nyitott a részleges frissítésre is
            return await AdatFrissit(id, dto);
        }
        /// Könyv törlése az adatbázisból Id alapján
        [HttpDelete("adatTorles/{id:int}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AdatTorles(int id)
        {
            var konyv = await _context.Konyv.FindAsync(id);

            if (konyv is null)
                return NotFound(new { uzenet = $"Nem található könyv {id} azonosítóval." });

            _context.Konyv.Remove(konyv);
            await _context.SaveChangesAsync();

            return Ok(new { uzenet = $"A könyv ({id}) sikeresen törölve lett az adatbázisból." });
        }

        private static KonyvValaszDto MapToValaszDto(Konyv k) => new()
        {
            Id = k.Id,
            Cim = k.Cim,
            KiadasIdeje = k.KiadasIdeje,
            Szerzo = k.Szerzo,
            Kategoria = k.Kategoria,
            Isbn = k.Isbn
        };
    }
}
