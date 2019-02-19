using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rendszam
{
    class Jarmu
    {
        public string rendszam;
        public DateTime forgErv;
        public string szin;
        public string tulajdonos;
        public string cim;

        public Jarmu() { }

        public Jarmu(string sor) {
            string[] sorElemek = sor.Split(';');
            rendszam = sorElemek[0];
            forgErv = DateTime.Parse(sorElemek[1]);
            szin = sorElemek[2];
            tulajdonos = sorElemek[3];
            cim = sorElemek[4];
        }
    }
}
