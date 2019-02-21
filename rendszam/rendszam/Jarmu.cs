using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rendszam
{
    class Jarmu
    {
        public string rendszam;//az autó rendszáma
        public DateTime forgErv;//a forgalmi érvényességének határideje
        public string szin;//az autó színe
        public string tulajdonos;//a tulajdonos neve
        public string cim;//a tulajdonos lakcíme

        public Jarmu() { }

        public Jarmu(string sor) {
            string[] sorElemek = sor.Split(';');//az adatsor szétválogatása a sorElemek tömbbe
            rendszam = sorElemek[0];
            forgErv = DateTime.Parse(sorElemek[1]);
            szin = sorElemek[2];
            tulajdonos = sorElemek[3];
            cim = sorElemek[4];
        }
    }
}
