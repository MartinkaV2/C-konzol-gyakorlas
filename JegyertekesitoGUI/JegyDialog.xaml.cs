using System.Windows;
using System.Windows.Controls;

namespace JegyertekesitoGUI
{
    /// <summary>
    /// Dialógusablak jegy hozzáadásához és szerkesztéséhez.
    /// Hozzáadásnál: üres mezőkkel nyílik meg.
    /// Szerkesztésnél: a kijelölt sor adataival töltődik fel.
    /// </summary>
    public partial class JegyDialog : Window
    {
        public Jegy Eredmeny { get; private set; }

        // ---- Hozzáadás mód (üres ablak) ----------------------------
        public JegyDialog()
        {
            InitializeComponent();
            Title = "Új jegy hozzáadása";
        }

        // ---- Szerkesztés mód (meglévő adatokkal) -------------------
        public JegyDialog(Jegy jegy)
        {
            InitializeComponent();
            Title = "Jegy szerkesztése";

            txtNev.Text       = jegy.Nev;
            txtAr.Text        = jegy.Ar.ToString();
            txtDarabszam.Text = jegy.Darabszam.ToString();

            // Az Id-t el kell menteni, hogy frissítéskor tudjuk melyik rekord
            Eredmeny = new Jegy { Id = jegy.Id };
        }

        // ---- Mentés gomb -------------------------------------------
        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            // Validáció
            if (string.IsNullOrWhiteSpace(txtNev.Text))
            {
                MessageBox.Show("A jegy neve nem lehet üres!", "Hiba",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtAr.Text, out decimal ar) || ar < 0)
            {
                MessageBox.Show("Az ár érvényes, nem negatív szám kell legyen!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtDarabszam.Text, out int db) || db < 0)
            {
                MessageBox.Show("A darabszám egész szám kell legyen, és nem lehet negatív!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int meglevoId = Eredmeny?.Id ?? 0;

            Eredmeny = new Jegy
            {
                Id        = meglevoId,
                Nev       = txtNev.Text.Trim(),
                Ar        = ar,
                Darabszam = db
            };

            DialogResult = true;
        }

        // ---- Mégse gomb --------------------------------------------
        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
