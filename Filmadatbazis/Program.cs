using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Filmadatbazis
{
    class Film
    {
        public string Cim { get; set; }
        public string Mufaj { get; set; }
        public string Rendezo { get; set; }
        public int MegjelenesiEv { get; set; }
        public double ImdbErtekeles { get; set; }

        public override string ToString()
        {
            // A tört értékelést egy tizedesjegyre formázzuk a kultúra beállításaitól függetlenül
            string ertekeles = ImdbErtekeles.ToString("0.0", CultureInfo.InvariantCulture);
            return $"{Cim} - {Mufaj} - {Rendezo} - {MegjelenesiEv} - {ertekeles}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Film> filmek = new List<Film>();

            // 1. Fájl beolvasása
            try
            {
                string[] sorok = File.ReadAllLines("filmek.txt");
                foreach (string sor in sorok)
                {
                    if (string.IsNullOrWhiteSpace(sor)) continue;
                    string[] reszek = sor.Split(';');
                    if (reszek.Length == 5)
                    {
                        Film f = new Film
                        {
                            Cim = reszek[0].Trim(),
                            Mufaj = reszek[1].Trim(),
                            Rendezo = reszek[2].Trim(),
                            MegjelenesiEv = int.Parse(reszek[3].Trim()),
                            // A tört értékelést invariáns kultúrával olvassuk (pont mint tizedeselválasztó)
                            ImdbErtekeles = double.Parse(reszek[4].Trim(), CultureInfo.InvariantCulture)
                        };
                        filmek.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a fájl beolvasása során: " + ex.Message);
                return;
            }

            // 2. Teljes tartalom kiírása
            Console.WriteLine("Filmadatbázis tartalma:");
            Console.WriteLine(new string('-', 70));
            foreach (var f in filmek)
            {
                Console.WriteLine(f);
            }
            Console.WriteLine();

            // 3. Filmek száma
            Console.WriteLine($"A filmadatbázisban található filmek száma: {filmek.Count}");
            Console.WriteLine();

            // 4. Műfajok szerinti csoportosítás
            Console.WriteLine("Műfajok szerinti csoportosítás:");
            var csoportok = filmek.GroupBy(f => f.Mufaj)
                                   .OrderBy(g => g.Key);
            foreach (var csoport in csoportok)
            {
                Console.WriteLine($"  {csoport.Key}: {csoport.Count()} film");
            }
            Console.WriteLine();

            // 5. Legmagasabb IMDb értékelésű film(ek)
            Console.WriteLine("Legmagasabb IMDb értékelésű film(ek):");
            double maxErtekeles = filmek.Max(f => f.ImdbErtekeles);
            var legjobbak = filmek.Where(f => Math.Abs(f.ImdbErtekeles - maxErtekeles) < 0.001)
                                   .ToList();
            foreach (var f in legjobbak)
            {
                Console.WriteLine($"  {f}");
            }
            Console.WriteLine();

            // 6. 2000 után megjelent filmek
            Console.WriteLine("2000 után megjelent filmek:");
            var kesobbi = filmek.Where(f => f.MegjelenesiEv > 2000)
                                 .OrderBy(f => f.MegjelenesiEv);
            foreach (var f in kesobbi)
            {
                Console.WriteLine($"  {f}");
            }
        }
    }
}