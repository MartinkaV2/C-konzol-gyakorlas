using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KonyvtarKezelo
{
    class Konyv
    {
        public string Cim { get; set; }
        public string Szerzo { get; set; }
        public string Kategoria { get; set; }
        public int KiadasiEv { get; set; }
        public int Oldalszam { get; set; }

        public override string ToString()
        {
            return $"{Cim} - {Szerzo} - {Kategoria} - {KiadasiEv} - {Oldalszam} oldal";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Konyv> konyvek = new List<Konyv>();

            // 1. Fájl beolvasása
            try
            {
                string[] sorok = File.ReadAllLines("konyvtar.txt");
                foreach (string sor in sorok)
                {
                    if (string.IsNullOrWhiteSpace(sor)) continue;
                    string[] reszek = sor.Split(';');
                    if (reszek.Length == 5)
                    {
                        Konyv k = new Konyv
                        {
                            Cim = reszek[0].Trim(),
                            Szerzo = reszek[1].Trim(),
                            Kategoria = reszek[2].Trim(),
                            KiadasiEv = int.Parse(reszek[3].Trim()),
                            Oldalszam = int.Parse(reszek[4].Trim())
                        };
                        konyvek.Add(k);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a fájl beolvasása során: " + ex.Message);
                return;
            }

            // 2. Teljes tartalom kiírása
            Console.WriteLine("1. Könyvtár tartalma:");
            Console.WriteLine(new string('-', 60));
            foreach (var k in konyvek)
            {
                Console.WriteLine(k);
            }
            Console.WriteLine();

            // 3. Könyvek száma
            Console.WriteLine($"2. A könyvek száma: {konyvek.Count}");
            Console.WriteLine();

            // 4. Kategóriák szerinti csoportosítás
            Console.WriteLine("3. Kategóriák szerinti bontás:");
            var csoportok = konyvek.GroupBy(k => k.Kategoria)
                                    .OrderBy(g => g.Key);
            foreach (var csoport in csoportok)
            {
                Console.WriteLine($"  {csoport.Key}: {csoport.Count()} könyv");
            }
            Console.WriteLine();

            // 5. Leghosszabb könyv(ek)
            Console.WriteLine("4. Leghosszabb könyv(ek):");
            int maxOldal = konyvek.Max(k => k.Oldalszam);
            var leghosszabbak = konyvek.Where(k => k.Oldalszam == maxOldal);
            foreach (var k in leghosszabbak)
            {
                Console.WriteLine($"  {k}");
            }
            Console.WriteLine();

            // 6. 1950 után kiadott könyvek
            Console.WriteLine("5. 1950 után kiadott könyvek:");
            var kesobbi = konyvek.Where(k => k.KiadasiEv > 1950)
                                  .OrderBy(k => k.KiadasiEv);
            foreach (var k in kesobbi)
            {
                Console.WriteLine($"  {k}");
            }
        }
    }
}