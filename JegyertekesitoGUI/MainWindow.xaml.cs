using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JegyertekesitoGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.AdatbazisLetrehozasa();
            Tablazat_Frissitese();
        }

        // ---- Táblázat frissítése -----------------------------------
        private void Tablazat_Frissitese()
        {
            List<Jegy> jegyek = DatabaseHelper.OssszesJegyLekereses();
            dgJegyek.ItemsSource = jegyek;
        }

        // ---- Hozzáadás gomb ----------------------------------------
        private void btnHozzaadas_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new JegyDialog { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                DatabaseHelper.JegyHozzaadasa(dialog.Eredmeny);
                Tablazat_Frissitese();
            }
        }

        // ---- Szerkesztés gomb --------------------------------------
        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            if (dgJegyek.SelectedItem is not Jegy kivalasztott)
            {
                MessageBox.Show("Kérem, jelöljön ki egy sort a táblázatban!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new JegyDialog(kivalasztott) { Owner = this };
            if (dialog.ShowDialog() == true)
            {
                DatabaseHelper.JegyFrissitese(dialog.Eredmeny);
                Tablazat_Frissitese();
            }
        }

        // ---- Törlés gomb -------------------------------------------
        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgJegyek.SelectedItem is not Jegy kivalasztott)
            {
                MessageBox.Show("Kérem, jelöljön ki egy sort a táblázatban!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var valasz = MessageBox.Show(
                $"Biztosan törli a következő jegyet?\n\"{kivalasztott.Nev}\"",
                "Törlés megerősítése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (valasz == MessageBoxResult.Yes)
            {
                DatabaseHelper.JegyTorlese(kivalasztott.Id);
                Tablazat_Frissitese();
            }
        }
    }
}
