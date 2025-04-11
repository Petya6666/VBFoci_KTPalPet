using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBfoci
{
    internal class Resztvevo
    {
        public string Orszag;
        public int Helyezes;
        public int VB_Idopont;
        public string VB_Hely;


        public Resztvevo(string sor) { 
               string[] adatok = sor.Split(';');
               Orszag = adatok[0];
               Helyezes = int.Parse(adatok[1]);
               VB_Idopont = int.Parse(adatok[2]);
               VB_Hely = adatok[3];
        
        
        }
    }
}
