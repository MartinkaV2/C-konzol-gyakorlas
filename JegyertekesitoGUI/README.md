# Jegyértékesítő GUI — Telepítési útmutató

## Előfeltételek
- .NET 8.0 SDK (Windows)
- Visual Studio 2022 VAGY Visual Studio Code C# kiterjessztéssel

## Projekt létrehozása (parancssorból)

```bash
dotnet new wpf -n JegyertekesitoGUI --framework net8.0
```
Ezután **cseréld le** a generált fájlokat a mellékelt fájlokra.

Majd add hozzá a NuGet csomagot:
```bash
dotnet add package Microsoft.Data.Sqlite --version 8.0.0
```

## Futtatás

```bash
dotnet run
```

## Fájlok leírása

| Fájl | Szerepkör |
|------|-----------|
| `Jegy.cs` | Model – az adatmodell osztály (Id, Nev, Ar, Darabszam) |
| `DatabaseHelper.cs` | Adatelérési réteg – SQLite CRUD műveletek |
| `MainWindow.xaml` | Főablak nézete – DataGrid és gombok |
| `MainWindow.xaml.cs` | Főablak logikája – eseménykezelők |
| `JegyDialog.xaml` | Dialógusablak nézete (Hozzáadás / Szerkesztés) |
| `JegyDialog.xaml.cs` | Dialógusablak logikája és validáció |
| `App.xaml / App.xaml.cs` | Alkalmazás belépési pontja |

## Adatbázis helye

Az SQLite adatbázis automatikusan létrejön itt:
```
%LOCALAPPDATA%\JegyManager\jegyek.db
```

## Funkciók

- **Hozzáadás** – Új jegy hozzáadás dialógussal
- **Szerkesztés** – Kijelölt jegy módosítása
- **Törlés** – Kijelölt jegy törlése megerősítéssel
- **Validáció** – Üres név, negatív ár/darabszám ellenőrzése
