using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//public static List<Jarmu> jarmuvek = new List<Jarmu>();
//public static List<string> korozott = new List<string>();
namespace rendszam
{
    class Program
    {
        static void Main(string[] args) {
            Adatbazis.Beolvas();
            List<string> rendsz = RendszamFelismero.Felismer();

         /* rendsz.Add("OCT-258");
            rendsz.Add("ABC-123");
            rendsz.Add("PEQ-536");
            rendsz.Add("NEJ-759");
            rendsz.Add("DYJ-856");
            rendsz.Add("JUZ-960");
            rendsz.Add("NINCS-BENNE");
            rendsz.Add("OCT-258");
            rendsz.Add("ABC-123");
            rendsz.Add("PEQ-536");
            rendsz.Add("NEJ-759");
            rendsz.Add("DYJ-856");
            rendsz.Add("JUZ-960");
            rendsz.Add("NINCS-BENNE");
            rendsz.Add("OCT-258");
            rendsz.Add("ABC-123");
            rendsz.Add("PEQ-536");
            rendsz.Add("NEJ-759");
            rendsz.Add("DYJ-856");
            rendsz.Add("JUZ-960"); */






            using (StreamWriter sw = new StreamWriter("osszesit.txt"))
            {
                for (int i = 0; i < 20; i++)
                {
                    int megtalalt = 0;
                    int j = 0;
                    megtalalt = 0;
                    j = 0;
                    string korozes = "nem korozik";
                    string ervenyesseg = "nem ervenyes";
                    bool nyilvantartasban = false;
                    foreach(Jarmu jarmu in Adatbazis.jarmuvek)
                    {
                        if (rendsz[i] == Adatbazis.jarmuvek[megtalalt].rendszam)
                        {
                            nyilvantartasban = true;

                            if(Adatbazis.jarmuvek[megtalalt].forgErv>=DateTime.Today)
                            {
                                ervenyesseg = "ervenyes";
                            }

                            foreach(string korozesek in Adatbazis.korozott)
                            {
                                if (Adatbazis.jarmuvek[megtalalt].rendszam == Adatbazis.korozott[j])
                                {
                                    korozes = "korozik";
                                }

                             j++;

                            }
                  
                        }
                            megtalalt++;
                    }
                    if(nyilvantartasban)
                    {
                        sw.WriteLine(rendsz[i] + ";" + ervenyesseg + ";" + korozes);
                    } else
                        {
                        sw.WriteLine(rendsz[i] + ";" + "nincs nyilvantartva");
                        }

                }
            }
            
        }
    }
}
