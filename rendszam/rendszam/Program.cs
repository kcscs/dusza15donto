using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rendszam
{
    class Program
    {
        static void Main(string[] args) {
            Adatbazis.Beolvas();
            List<string> rendsz = RendszamFelismero.Felismer();
            
        }
    }
}
