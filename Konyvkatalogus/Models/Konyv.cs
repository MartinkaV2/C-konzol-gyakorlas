using System.ComponentModel.DataAnnotations;

namespace Konyvkatalogus.Models
{
    public class Konyv
    {
        /// Automatikusan generált elsődleges kulcs
        public int Id { get; set; }

        /// A könyv címe
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        [StringLength(300, ErrorMessage = "A cím maximum 300 karakter lehet.")]
        public string Cim { get; set; } = string.Empty;

        /// A könyv megjelenésének dátuma
        [Required(ErrorMessage = "A kiadás ideje megadása kötelező.")]
        public DateTime KiadasIdeje { get; set; }

        /// A könyv szerzője
        [Required(ErrorMessage = "A szerző megadása kötelező.")]
        [StringLength(200, ErrorMessage = "A szerző neve maximum 200 karakter lehet.")]
        public string Szerzo { get; set; } = string.Empty;

        /// A könyv kategóriája
        [Required(ErrorMessage = "A kategória megadása kötelező.")]
        [StringLength(100, ErrorMessage = "A kategória maximum 100 karakter lehet.")]
        public string Kategoria { get; set; } = string.Empty;

        /// ISBN azonosító
        [Required(ErrorMessage = "Az ISBN megadása kötelező.")]
        [StringLength(20, ErrorMessage = "Az ISBN maximum 20 karakter lehet.")]
        public string Isbn { get; set; } = string.Empty;
    }
}
