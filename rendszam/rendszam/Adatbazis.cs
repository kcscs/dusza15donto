using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace rendszam
{
    static class Adatbazis
    {
        public static List<Jarmu> jarmuvek = new List<Jarmu>();
        public static List<string> korozott = new List<string>();

        public static void Beolvas() { //beolvasas
            using (StreamReader sr = new StreamReader("jarmu.txt"))
            {
                string fejLec = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string adatSor = sr.ReadLine();
                    Jarmu j = new Jarmu(adatSor);
                    jarmuvek.Add(j);
                }
            }
            using (StreamReader sr2 = new StreamReader("korozott.txt"))
            {
                while (!sr2.EndOfStream)
                {
                    string rendSzam;
                    rendSzam = sr2.ReadLine();
                    korozott.Add(rendSzam);
                    
                }
            }
        }
    }
}
