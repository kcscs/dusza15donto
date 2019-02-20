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

            /*rendsz.Add("OCT-258");
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
            rendsz.Add("JUZ-960");*/






            Console.OutputEncoding = System.Text.Encoding.UTF8;
            using (StreamWriter sw = new StreamWriter("osszesit.txt"))
            {
                Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("osszesit.txt\n");
                Console.ForegroundColor = ConsoleColor.White;
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
                        Console.WriteLine(rendsz[i] + ";" + ervenyesseg + ";" + korozes);
                    } else
                        {
                        sw.WriteLine(rendsz[i] + ";" + "nincs nyilvantartva");
                        Console.WriteLine(rendsz[i] + ";" + "nincs nyilvantartva");
                        }

                }
            }
            //Színek megtalálása
            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Legkorozottebb szinek:\n");
            Console.ForegroundColor = ConsoleColor.White;
            List<string> szinek = new List<string>();
            int kint = 0;
            foreach(Jarmu jarmu in Adatbazis.jarmuvek)
            {
                    szinek.Add(Adatbazis.jarmuvek[kint].szin);
                    kint++;
            }
            for(int i=0;i<szinek.Count;i++)
            {
                for(int j=0;j<szinek.Count;j++)
                {
                    if(szinek[i]==szinek[j] && i!=j)
                    {
                        szinek.RemoveAt(i);
                        i = 0;
                        break;
                    }
                }
            }
            int[] megszamol = new int[szinek.Count];

            for(int i=0;i<szinek.Count;i++)
            {
                megszamol[i] = 0;
            }

            int osszesen = 0;

            for (int i = 0; i < szinek.Count; i++)
            {
                foreach (Jarmu jarmu in Adatbazis.jarmuvek)
                {
                    if (szinek[i] == jarmu.szin)
                    {
                        megszamol[i]++;
                        osszesen++;
                    }
                }
            }
            int[] eredetiMegszamol = new int[szinek.Count];
            Array.Copy(megszamol, eredetiMegszamol, szinek.Count);
            Array.Sort<int>(megszamol,
                   new Comparison<int>(
                           (i1, i2) => i2.CompareTo(i1)
                   ));

            int x = 0;
            
            for (int i=0;i<szinek.Count;i++)
            {
                x = 0;
                foreach(var szin in szinek)
                {
                    if(megszamol[i]==eredetiMegszamol[x])
                    {
                         Console.WriteLine(szinek[x] + ": " + Math.Round(megszamol[i] * 100.0 / osszesen, 0) + "%");
                        megszamol[i] = -1; //Ha két ugyanolyan % van, akkor ne azt találja meg amit már egyszer megtalált
                    }
                    x++;
                }
            }
            


            List<string> razzia = new List<string>();
            razzia = Adatbazis.razziaHely();
            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Razziahelyek:\n");
            Console.ForegroundColor = ConsoleColor.White;
            if (razzia.Count > 0) {
                for (int i = 0; i < razzia.Count; i++) {
                    Console.WriteLine(razzia[i]);
                }
            } else {
                Console.WriteLine("Nincs kiemelt helyszín.");
            }
            Console.WriteLine("_____________________");
            Console.ReadKey();
            
        }
    }
}
