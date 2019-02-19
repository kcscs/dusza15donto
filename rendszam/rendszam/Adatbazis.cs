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
        public static List<string> razziaHelyek = new List<string>();

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
        static List<string> razziaHely()
        {
            int maxVaros=1;
            List<string> razziaHely=new List<string>();
            Dictionary<string, int> varosSzam = new Dictionary<string, int>();
            foreach (var i in korozott)
            {
                foreach (var j in jarmuvek)
                {
                    if(i==j.rendszam)
                    {
                        string[] elemek=j.cim.Split(' ');
                        string varosNev = elemek[0];
                        razziaHelyek.Add(varosNev);

                    }
                }
            }
            foreach (var k in razziaHelyek)
            {
                if(varosSzam.ContainsKey(k))
                {
                    varosSzam[k]++;
                }
                else
                {
                    varosSzam.Add(k, 1);
                }
            }
            foreach (var l in varosSzam)
            {
                if(l.Value>maxVaros)
                {
                    maxVaros = l.Value;
                    razziaHely.Clear();
                    razziaHely.Add(l.Key);
                }
                else if(l.Value==maxVaros)
                {
                    razziaHely.Add(l.Key);
                }
            }
            return razziaHely;
        }
    }
}
