using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VBfoci
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        List<Resztvevo> resztvevok = new List<Resztvevo>();
        List<string> mentendoSorok = new List<string>();

        private void beolvasBtn_Click(object sender, RoutedEventArgs e)
        {
            string fajlnev = fajlbeolvasTxt.Text;
            StreamReader sr = new StreamReader(fajlnev, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                resztvevok.Add(new Resztvevo(line));
            }
            sr.Close();
            beolvasTxtBx.Text = "A beolvasás sikeresen megtörtént.";
            int min = resztvevok.Min(r => r.VB_Idopont);
            beolvasTxtBx.Text += $"\nLegrégebbi VB: {min}";

            int szamlalo = 0;
            int osszeseredmeny = 0;
            for (int i = 0; i < resztvevok.Count; i++)
            {
                if (resztvevok[i].Orszag == "Magyarország")
                {
                    szamlalo++;
                    osszeseredmeny += resztvevok[i].Helyezes;
                }
            }
            beolvasTxtBx.Text += $"\nMagyaroszág átlaghelyezése: {osszeseredmeny/szamlalo}";
        }

        private void kiirasBtn_Click(object sender, RoutedEventArgs e)
        {
            string fajlnev = kiirasTxtbox.Text;
            using (StreamWriter sw = new StreamWriter(fajlnev))
            {
                foreach (string sor in mentendoSorok)
                {
                    sw.WriteLine(sor);
                }
            }
        }

        private void OKeresesBtn_Click(object sender, RoutedEventArgs e)
        {
            string orszag = OTextbox.Text;
            beolvasTxtBx.Text = $"{orszag} szereplései:\n";

            mentendoSorok.Add($"{orszag} szereplései:");

            for (int i = 0; i < resztvevok.Count; i++)
            {
                if (resztvevok[i].Orszag == orszag)
                {
                    string sor = $"Év: {resztvevok[i].VB_Idopont}, Helyezés: {resztvevok[i].Helyezes}";
                    if (top.IsChecked == true)
                    {
                        if (resztvevok[i].Helyezes <= 5)
                        {
                            beolvasTxtBx.Text += $"Év: {resztvevok[i].VB_Idopont}, Helyezés: {resztvevok[i].Helyezes}\n";
                            mentendoSorok.Add(sor);
                        }
                    }
                    else
                    {
                        beolvasTxtBx.Text += $"Év: {resztvevok[i].VB_Idopont}, Helyezés: {resztvevok[i].Helyezes}\n";
                        mentendoSorok.Add(sor);
                    }
                }
            }
            mentendoSorok.Add(""); // sorkihagyás a végére
        }

        private void HelyezesBtn_Click(object sender, RoutedEventArgs e)
        {
            int ev = Convert.ToInt32(evTxt.Text);
            string eredmeny = $"Szűrés év szerint: {ev}\n";

            foreach (var r in resztvevok)
            {
                if (r.VB_Idopont == ev)
                {
                    if (top3.IsChecked == true && r.Helyezes >= 1 && r.Helyezes <= 3)
                    {
                        eredmeny += $"{r.Orszag} - {r.Helyezes}\n";
                    }
                    else if (top10.IsChecked == true && r.Helyezes >= 4 && r.Helyezes <= 10)
                    {
                        eredmeny += $"{r.Orszag} - {r.Helyezes}\n";
                    }
                    else if (mindenki.IsChecked == true)
                    {
                        eredmeny += $"{r.Orszag} - {r.Helyezes}\n";
                    }
                }
            }

            beolvasTxtBx.Text = eredmeny;
            mentendoSorok.Add(eredmeny); 
        }

        private void HelyzetKeresesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (helyezesCombo.SelectedItem is ComboBoxItem selectedItem)
            {
                int keresettHelyezes = int.Parse(selectedItem.Content.ToString());
                beolvasTxtBx.Text = $"{keresettHelyezes}. helyezettek listája:\n";
                mentendoSorok.Add($"{keresettHelyezes}. helyezettek listája:"); 

                var evSzerint = resztvevok
                    .Where(r => r.Helyezes == keresettHelyezes)
                    .OrderBy(r => r.VB_Idopont);

                foreach (var r in evSzerint)
                {
                    string sor = $"{r.VB_Idopont}: {r.Orszag}";
                    beolvasTxtBx.Text += sor + "\n";
                    mentendoSorok.Add(sor); 
                }
            }
        }
    }
}
