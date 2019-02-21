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
            Adatbazis.Beolvas();  // Járművek beolvasása
            List<string> rendsz = RendszamFelismero.Felismer(); // Rendsz lista feltöltése a felismert rendszámokkal

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






            Console.OutputEncoding = System.Text.Encoding.UTF8; // Ez a sor által lehetséges az ' ‾ ' karakterek kiíratása
            using (StreamWriter sw = new StreamWriter("osszesit.txt")) // osszesit.-txt létrehozása, feltöltése
            {
                Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("osszesit.txt\n");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 20; i++) 
                {
                    int megtalalt = 0; // Ha meglett találva az adatbázisban, akkor elmenti, hogy hanyadik sorban van   
                    int j = 0;
                    megtalalt = 0; 
                    j = 0;
                    string korozes = "nem korozik";
                    string ervenyesseg = "nem ervenyes";
                    bool nyilvantartasban = false;
                    foreach(Jarmu jarmu in Adatbazis.jarmuvek)
                    {
                        if (rendsz[i] == Adatbazis.jarmuvek[megtalalt].rendszam) // Az adott rendszám megtalálása az adatbázisban
                        {
                            nyilvantartasban = true; // Ha megtalálható az adatbásisban, akkor kerüljön bele a nyílvántartásba

                            if(Adatbazis.jarmuvek[megtalalt].forgErv>=DateTime.Today)
                            {
                                ervenyesseg = "ervenyes"; // Ha a forgalmi érvényességi nagyobb mint a mai nap, akkor legyen érvényes (alapból nem érvényes)
                            }

                            foreach(string korozesek in Adatbazis.korozott)
                            {
                                if (Adatbazis.jarmuvek[megtalalt].rendszam == Adatbazis.korozott[j])
                                {
                                    korozes = "korozik"; // Ha az adott jármú rendszáma megtalálható a körözöttek között, akkor legyen körözött (alapból nem körözött)
                                }

                             j++;

                            }
                  
                        }
                            megtalalt++;
                    }
                    if(nyilvantartasban) // Jármű adatainak kiírása az osszesit.txt-be valamint a consolera
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
            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾"); 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Legkorozottebb szinek:\n");
            Console.ForegroundColor = ConsoleColor.White;
            List<string> szinek = new List<string>();
            int kint = 0;
            foreach(Jarmu jarmu in Adatbazis.jarmuvek)
            {
                    szinek.Add(Adatbazis.jarmuvek[kint].szin); // Színek hozzáadása egy listához
                    kint++;
            }
            for(int i=0;i<szinek.Count;i++)
            {
                for(int j=0;j<szinek.Count;j++)
                {
                    if(szinek[i]==szinek[j] && i!=j)
                    {
                        szinek.RemoveAt(i); // Ha már létezik az adott szín, akkor vegye ki a listából, így minden színből csak egy darab lesz
                        i = 0;
                        break;
                    }
                }
            }
            int[] megszamol = new int[szinek.Count]; // Ez a tömb tartalmazni fogja, hogy melyik színből mennyi van

            for(int i=0;i<szinek.Count;i++)
            {
                megszamol[i] = 0; // Tömb kiürítése 
            }

            int osszesen = 0;

            for (int i = 0; i < szinek.Count; i++)
            {
                foreach (Jarmu jarmu in Adatbazis.jarmuvek)
                {
                    if (szinek[i] == jarmu.szin)
                    {
                        megszamol[i]++; // A megszámol tömb feltöltése
                        osszesen++;
                    }
                }
            }
            int[] eredetiMegszamol = new int[szinek.Count]; // Ez a tömb tartalmazza a megszámol tömb eredeti formáját (mivel később a megszámol tömb rendeződk)
            Array.Copy(megszamol, eredetiMegszamol, szinek.Count); // Megszámol tömb bemásolása az eredeti tömbbe
            Array.Sort<int>(megszamol, // Csökkenő sorrendben rendezi a megszámol tömböt 
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
                         Console.WriteLine(szinek[x] + ": " + Math.Round(megszamol[i] * 100.0 / osszesen, 0) + "%"); // Az összes szín kiírása kerekített százalékokkal
                        megszamol[i] = -1; //Ha két ugyanolyan % van, akkor ne azt találja meg amit már egyszer megtalált
                    }
                    x++;
                }
            }
            


            List<string> razzia = new List<string>(); // Razziahelyek listája
            razzia = Adatbazis.razziaHely(); // Razzia lista feltöltése az 'Adatbázis' osztályban megírt függvénnyel
            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾"); 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Razziahelyek:\n");
            Console.ForegroundColor = ConsoleColor.White;
            if (razzia.Count > 0) {
                for (int i = 0; i < razzia.Count; i++) {
                    Console.WriteLine(razzia[i]); // Ha vannak razziahelyek, akkor írja ki őket
                }
            } else {
                Console.WriteLine("Nincs kiemelt helyszín."); // Ha nincsenek egy razziahely sem, akkor az jelezze
            }
            Console.WriteLine("_____________________");
            Console.ReadKey();
            
        }
    }
}
