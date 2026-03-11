namespace FilmLista.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public string Rendezo { get; set; }
        public string Mufaj { get; set; }
        public int MegjelenesiEv { get; set; }
        public double IMDBPontszam { get; set; }
    }
}