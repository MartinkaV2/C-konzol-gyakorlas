using System.ComponentModel.DataAnnotations;

namespace Konyvkatalogus.DTOs
{

    /// DTO a könyv létrehozásához (POST /adatHozzad)
    public class KonyvLetrehozasDto
    {
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Cim { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadás ideje megadása kötelező.")]
        public DateTime KiadasIdeje { get; set; }

        [Required(ErrorMessage = "A szerző megadása kötelező.")]
        public string Szerzo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kategória megadása kötelező.")]
        public string Kategoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Az ISBN megadása kötelező.")]
        public string Isbn { get; set; } = string.Empty;
    }

    /// DTO a könyv frissítéséhez (PUT/PATCH /adatFrissit/{id})
    public class KonyvFrissitesDto
    {
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Cim { get; set; } = string.Empty;

        [Required(ErrorMessage = "A szerző megadása kötelező.")]
        public string Szerzo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kategória megadása kötelező.")]
        public string Kategoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Az ISBN megadása kötelező.")]
        public string Isbn { get; set; } = string.Empty;
    }

    /// Válasz DTO – a könyv adatait tartalmazza
    public class KonyvValaszDto
    {
        public int Id { get; set; }
        public string Cim { get; set; } = string.Empty;
        public DateTime KiadasIdeje { get; set; }
        public string Szerzo { get; set; } = string.Empty;
        public string Kategoria { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
    }
}
