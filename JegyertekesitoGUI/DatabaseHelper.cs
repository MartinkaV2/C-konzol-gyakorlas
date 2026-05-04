using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace JegyertekesitoGUI
{
    public static class DatabaseHelper
    {
        private static readonly string DbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "JegyManager",
                "jegyek.db");

        private static string ConnectionString => $"Data Source={DbPath}";

        // Adatbázis létrehozása az alkalmazás első indításakor
        public static void AdatbazisLetrehozasa()
        {
            string dir = Path.GetDirectoryName(DbPath)!;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Jegyek
                (
                    Id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nev        TEXT    NOT NULL,
                    Ar         REAL    NOT NULL,
                    Darabszam  INTEGER NOT NULL
                );";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        // Összes jegy lekérdezése -> List<Jegy>
        public static List<Jegy> OssszesJegyLekereses()
        {
            var lista = new List<Jegy>();

            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand("SELECT Id, Nev, Ar, Darabszam FROM Jegyek ORDER BY Id;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Jegy
                {
                    Id        = reader.GetInt32(0),
                    Nev       = reader.GetString(1),
                    Ar        = reader.GetDecimal(2),
                    Darabszam = reader.GetInt32(3)
                });
            }

            return lista;
        }

        // Új jegy hozzáadása az adatbázisba
        public static void JegyHozzaadasa(Jegy j)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand(
                "INSERT INTO Jegyek (Nev, Ar, Darabszam) VALUES (@nev, @ar, @db);", conn);
            cmd.Parameters.AddWithValue("@nev", j.Nev);
            cmd.Parameters.AddWithValue("@ar",  j.Ar);
            cmd.Parameters.AddWithValue("@db",  j.Darabszam);
            cmd.ExecuteNonQuery();
        }

        // Meglévő rekord módosítása az Id alapján
        public static void JegyFrissitese(Jegy j)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand(
                "UPDATE Jegyek SET Nev=@nev, Ar=@ar, Darabszam=@db WHERE Id=@id;", conn);
            cmd.Parameters.AddWithValue("@nev", j.Nev);
            cmd.Parameters.AddWithValue("@ar",  j.Ar);
            cmd.Parameters.AddWithValue("@db",  j.Darabszam);
            cmd.Parameters.AddWithValue("@id",  j.Id);
            cmd.ExecuteNonQuery();
        }

        // Törli a megadott Id-jú rekordot
        public static void JegyTorlese(int id)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            using var cmd = new SqliteCommand("DELETE FROM Jegyek WHERE Id=@id;", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
