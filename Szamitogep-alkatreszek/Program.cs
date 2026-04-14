using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Szamitogep_alkatreszek
{
    class Alkateresz
    {
        public string Nev { get; set; }
        public string Gyarto { get; set; }
        public string Kategoria { get; set; }
        public int MegjelenesiEv { get; set; }
        public int Ar { get; set; }

        public Alkateresz(string nev, string gyarto, string kategoria, int ev, int ar)
        {
            Nev = nev;
            Gyarto = gyarto;
            Kategoria = kategoria;
            MegjelenesiEv = ev;
            Ar = ar;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Alkateresz> alkatreszek = new List<Alkateresz>();

            string fajlnev = "alkatreszek.txt";

            try
            {
                string[] sorok = File.ReadAllLines(fajlnev);

                foreach (string sor in sorok)
                {
                    if (string.IsNullOrWhiteSpace(sor)) continue;

                    string[] adatok = sor.Split(';');
                    if (adatok.Length == 5)
                    {
                        string nev = adatok[0].Trim();
                        string gyarto = adatok[1].Trim();
                        string kategoria = adatok[2].Trim();
                        int ev = int.Parse(adatok[3].Trim());
                        int ar = int.Parse(adatok[4].Trim());

                        alkatreszek.Add(new Alkateresz(nev, gyarto, kategoria, ev, ar));
                    }
                }


                Console.WriteLine("=== Számítógép-alkatrészek adatai ===");

                foreach (var alk in alkatreszek)
                {
                    Console.WriteLine($"{alk.Nev} - {alk.Gyarto} - {alk.Kategoria} - {alk.MegjelenesiEv} - {alk.Ar} Ft");
                }

                Console.WriteLine($"\nÖsszes alkatrész: {alkatreszek.Count}");

                Console.WriteLine("\n=== Kategória szerinti csoportosítás ===");

                var csoportositas = alkatreszek
                    .GroupBy(a => a.Kategoria)
                    .OrderBy(g => g.Key);

                foreach (var csoport in csoportositas)
                {
                    Console.WriteLine($"{csoport.Key}: {csoport.Count()} db");
                }

                Console.WriteLine("\n=== Legdrágább alkatrész(ek) ===");

                int legdragabbAr = alkatreszek.Max(a => a.Ar);
                var legdragabbak = alkatreszek.Where(a => a.Ar == legdragabbAr);

                foreach (var alk in legdragabbak)
                {
                    Console.WriteLine($"{alk.Nev} - {alk.Gyarto} - {alk.Kategoria} - {alk.MegjelenesiEv} - {alk.Ar} Ft");
                }

                Console.WriteLine("\n=== 2020 után megjelent alkatrészek ===");

                var ujabbak = alkatreszek.Where(a => a.MegjelenesiEv > 2020)
                                         .OrderBy(a => a.Nev);

                foreach (var alk in ujabbak)
                {
                    Console.WriteLine($"{alk.Nev} - {alk.Gyarto} - {alk.Kategoria} - {alk.MegjelenesiEv} - {alk.Ar} Ft");
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Hiba: A(z) {fajlnev} fájl nem található!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }

            Console.WriteLine("\nNyomj egy billentyűt a kilépéshez...");
            Console.ReadKey();
        }
    }
}