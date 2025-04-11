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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Resztvevo> resztvevok = new List<Resztvevo>();
            StreamReader sr = new StreamReader("VBfoci.csv");


            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                resztvevok.Add(new Resztvevo(line));
            }
            sr.Close();

        }
    }
}
