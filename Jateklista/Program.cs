using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Termek
{
    public string Nev { get; set; }
    public string Kategoria { get; set; }
    public string Gyarto { get; set; }
    public int Ar { get; set; }
    public int Keszlet { get; set; }

    public Termek(string nev, string kategoria, string gyarto, int ar, int keszlet)
    {
        Nev = nev;
        Kategoria = kategoria;
        Gyarto = gyarto;
        Ar = ar;
        Keszlet = keszlet;
    }
}

class Program
{
    static void Main()
    {
        List<Termek> termekek = new List<Termek>();

        // 1. Fájl beolvasása
        try
        {
            using (StreamReader sr = new StreamReader("jatekok.txt"))
            {
                string sor;
                while ((sor = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(sor)) continue;
                    string[] reszek = sor.Split(';');
                    if (reszek.Length == 5)
                    {
                        string nev = reszek[0].Trim();
                        string kategoria = reszek[1].Trim();
                        string gyarto = reszek[2].Trim();
                        int ar = int.Parse(reszek[3].Trim());
                        int keszlet = int.Parse(reszek[4].Trim());
                        termekek.Add(new Termek(nev, kategoria, gyarto, ar, keszlet));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Hiba a fájl beolvasásakor: " + e.Message);
            return;
        }

        // 2. Teljes lista kiírása
        Console.WriteLine("Játékbolt termékei:");
        foreach (var t in termekek)
        {
            Console.WriteLine($"{t.Nev} - {t.Kategoria} - {t.Gyarto} - {t.Ar} Ft - {t.Keszlet} db");
        }

        // 3. Összes termék száma
        Console.WriteLine($"\nÖsszes termék száma: {termekek.Count}");

        // 4. Kategóriánkénti csoportosítás
        var kategoriaCount = new Dictionary<string, int>();
        foreach (var t in termekek)
        {
            if (kategoriaCount.ContainsKey(t.Kategoria))
                kategoriaCount[t.Kategoria]++;
            else
                kategoriaCount[t.Kategoria] = 1;
        }
        Console.WriteLine("\nTermékek kategória szerinti csoportosítása:");
        foreach (var kvp in kategoriaCount)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} db");
        }

        // 5. Legdrágább termék(ek)
        int maxAr = termekek.Max(t => t.Ar);
        Console.WriteLine("\nLegdrágább termék(ek):");
        foreach (var t in termekek.Where(t => t.Ar == maxAr))
        {
            Console.WriteLine($"{t.Nev} - {t.Kategoria} - {t.Gyarto} - {t.Ar} Ft");
        }

        // 6. 5 darabnál kevesebb készleten lévő termékek
        Console.WriteLine("\n5 darabnál kevesebb készleten lévő termékek:");
        foreach (var t in termekek.Where(t => t.Keszlet < 5))
        {
            Console.WriteLine($"{t.Nev} - {t.Kategoria} - {t.Gyarto} - {t.Ar} Ft - {t.Keszlet} db");
        }

        Console.ReadKey();
    }
}