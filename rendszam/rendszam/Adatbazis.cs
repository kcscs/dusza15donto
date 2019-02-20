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
                string fejLec = sr.ReadLine();// az első sor még nem adat
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
                    korozott.Add(rendSzam);//lista feltöltése
                    
                }
            }
        }
        public static List<string> razziaHely()//megkeresi a razziára leginkább alkalmas területeket
        {
            int maxVaros=1;
            List<string> razziaHely=new List<string>();//azok a városok ahol a körözött autók tulajai laknak
            Dictionary<string, int> varosSzam = new Dictionary<string, int>();//melyik városba(key) mennyi körözött autóval rendelkező tulaj(value) lakik
            foreach (var i in korozott)
            {
                foreach (var j in jarmuvek)
                {
                    if(i==j.rendszam)
                    {
                        string[] elemek=j.cim.Split(' ');//kiveszi az adott címből a várost
                        string varosNev = elemek[0];
                        razziaHelyek.Add(varosNev);//az adott körözött autó tulajának városát adja hozzá

                    }
                }
            }
            foreach (var k in razziaHelyek)//végigmegy a javasolt razziahelyeken
            {
                if(varosSzam.ContainsKey(k))
                {
                    varosSzam[k]++;//ha már tartalmazza a lista akkor növeli a számát 
                }
                else
                {
                    varosSzam.Add(k, 1);//ha még nem akkor hozzáadja a várost
                }
            }
            foreach (var l in varosSzam)
            {
                if(l.Value>maxVaros)//megkeresi a legnagyobb értékhez tartozó várost és feltölti a razziaHely listába
                {
                    maxVaros = l.Value;
                    razziaHely.Clear();
                    razziaHely.Add(l.Key);
                }
                else if(l.Value==maxVaros)//ha több ilyen város van feltölti az összeset a razziaHely listába
                {
                    razziaHely.Add(l.Key);
                }
            }
            return razziaHely;//a függvény a javasolt razziahelyek listájával tér vissza
        }
    }
}
