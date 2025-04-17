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

        private void beolvasBtn_Click(object sender, RoutedEventArgs e)
        {
            string fajlnev = "VBfoci.csv";
            StreamReader sr = new StreamReader(fajlnev, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                resztvevok.Add(new Resztvevo(line));
            }
            sr.Close();
            beolvasTxtBx.Text = "A beolvasás sikeresen megtörtént.";
        }

        private void kiirasBtn_Click(object sender, RoutedEventArgs e)
        {
            string fajlnev = kiirasTxtbox.Text;
            StreamWriter sw = new StreamWriter(fajlnev);
            sw.Close();
        }
    }
}
