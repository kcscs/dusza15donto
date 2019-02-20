using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace rendszam
{
    static class RendszamFelismero
    {
        /// <summary>
        /// Minta karakterek szelessege
        /// </summary>
        const int alapSzelesseg = 5;
        /// <summary>
        /// Minta karakterek magassaga
        /// </summary>
        const int alapMagassag = 7;
        /// <summary>
        /// Karakterek szama egy rendszamban
        /// </summary>
        const int alapKarDb = 7;

        /// <summary>
        /// Betuk kepet tartalmazo fajl relativ eleresi utja
        /// </summary>
        const string betuKep = "karakterek\\alapbetu.txt";
        /// <summary>
        /// Szamok kepet tartalmazo fajl relativ eleresi utja
        /// </summary>
        const string szamKep = "karakterek\\alapszam.txt";
        /// <summary>
        /// Kotojel kepet tartalmazo fajl relativ eleresi utja
        /// </summary>
        const string kotojelKep = "karakterek\\kotojel.txt";

        /// <summary>
        /// A karakterek es a hozzajuk tartozo beolvasott alap meretu kep
        /// </summary>
        static Dictionary<char, char[,]> karakterek = new Dictionary<char, char[,]>();

        public static List<string> Felismer() {
            List<string> rendszamok = new List<string>(); //ide kerulnek majd a rendszamokat tartalmazo string-ek

            //minta karatkerek beolvasasa
            BeolvasKarakterek(betuKep);
            BeolvasKarakterek(szamKep);
            BeolvasKotojel();

            //rendszam fajlok megkeresese
            string[] rendszamFajlok = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"??.txt");

            foreach (string fajl in rendszamFajlok) {
                using(StreamReader sr = new StreamReader(fajl)) {   //Aktualis fajl beolvasasa es a kep letarolasa egy karakter matrixba
                    List<string> ls = new List<string>();   //a kep sorai
                    while (!sr.EndOfStream)
                        ls.Add(sr.ReadLine());
                    char[,] kep = new char[ls.Count, ls[0].Length];
                    for (int i = 0; i < ls.Count; i++) {    //atiras matrixba
                        for (int j = 0; j < ls[0].Length; j++) {
                            kep[i, j] = ls[i][j];
                        }
                    }

                    rendszamok.Add(getNumber(kep)); // rendszam meghatarozasa
                }
            }

            return rendszamok;
        }

        /// <summary>
        /// egy kep megadott reszerol hatarozza meg hogy az milyen karaktert abrazol
        /// </summary>
        /// <param name="kep">A karaktert (is) tartalmazo kep</param>
        /// <param name="bal">A karakter inenn kezdodik jobbra</param>
        /// <param name="felso">A karaktert innen kezdodik lefele</param>
        /// <param name="szel">A karakter szelessege</param>
        /// <param name="mag">A karakter magassaga</param>
        /// <returns></returns>
        private static char getChar(char[,] kep, int bal, int felso, int szel, int mag) {   
            foreach (var kar in karakterek) {   // Az osszes ismert karakterrel osszehasonlitjuk
                bool jo = true;
                for (int i = felso; i < mag+felso && jo; i++) { // ha mar elterest talalunk nincs ertelme tovabb menni, ezert a ciklusfeltetelhez hozzaadjuk jo-t is
                    for (int j = bal; j < szel+bal && jo; j++) {
                        // a karakter (i,j) koordinatajanak megfelelo minta karakter beli (x,y) koordinata meghatarozasa
                        int y = (i - felso) * alapMagassag / mag;   //atmeretezes fuggolegesen
                        int x = (j - bal) * alapSzelesseg / szel;   //atmeretezes vizszintesen
                        if (char.ToUpper(kep[i, j]) != char.ToUpper(kar.Value[y, x]))   // Az egymasnak megfelelo koordinatak "pixelenek" osszehasonlitasa - nagybetu/kisbetu egynek szamit (a fajlban van ahol kis x van X helyett)
                            jo = false;
                    }
                }

                if (jo)
                    return kar.Key;
            }

            return ' ';
        }

        /// <summary>
        /// Megallapitja, hogy a megadott kep milyen rendszamot abrazol
        /// </summary>
        /// <param name="kep">A kep mint karakter matrix</param>
        /// <returns></returns>
        private static string getNumber(char[,] kep) {
            int m = kep.GetLength(0);   // kep magassaga
            int sz = kep.GetLength(1); // kep szelessege

            // egy karakter szelessege (mivel a karakter magassaga azonos a kep magassagaval es a magassag/szelesseg aranya nem valtozott a meretezes soran
            int karSzel = m * alapSzelesseg / alapMagassag;
            int koz = (sz-karSzel*7)/6;   //karakterek kozotti tavolsag

            StringBuilder sb = new StringBuilder(); //Ebbe kerulnek majd a meghatarozott karakterek

            for (int i = 0; i < alapKarDb; i++) {   // a rendszam karakterei azonos szelesseguek ezert vegig lehet "lepkedni" rajtuk
                sb.Append(getChar(kep, (karSzel + koz) * i, 0, karSzel, m));    //Az aktualis karakter meghatarozasa
            }

            return sb.ToString();
        }

        /// <summary>
        /// A mintakarakterek beolvasasat vegzi
        /// </summary>
        /// <param name="file"></param>
        private static void BeolvasKarakterek(string file) {
            string str;
            char c = ' ';
            using (StreamReader sr = new StreamReader(file)) {
                while (!sr.EndOfStream && c != 'Z') {   // 'Z' kilepesi feltetel szukseges mert a fajl vegen a 'Z' karakter utan nem csak 1 ures sor van
                    c = sr.ReadLine()[0];   //a karakter amihez a kep tartozik
                    char[,] kep = new char[alapMagassag,alapSzelesseg]; // a kepet karakter matrix-kent taroljuk
                    for (int i = 0; i < alapMagassag; i++) {    //kep beolvasasa es matrixba tarolasa
                        str = sr.ReadLine();
                        for (int j = 0; j < alapSzelesseg; j++) {
                            kep[i, j] = str[j];
                        }
                    }
                    karakterek.Add(c, kep); //hozzaadjuk az ismert karakterekhez
                    sr.ReadLine();  //ures sor a karakterek kozott
                }
            }
        }

        /// <summary>
        /// A minta kotojel beolvasasat vegzi
        /// </summary>
        private static void BeolvasKotojel() {
            using (StreamReader sr = new StreamReader(kotojelKep)) {
                char[,] kep = new char[alapMagassag, alapSzelesseg];    // ide kerul majd a kotojel kepe
                for (int i = 0; i < alapMagassag; i++) {    // kep beolvasasa matrixba
                    string str = sr.ReadLine();
                    for (int j = 0; j < alapSzelesseg; j++) {
                        kep[i, j] = str[j];
                    }
                }
                karakterek.Add('-', kep);   //hozzaadjuk az ismert karakterekhez
            }
        }
    }
}
