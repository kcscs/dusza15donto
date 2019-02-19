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
        const int alapSzelesseg = 5;
        const int alapMagassag = 7;
        const int alapKoz = 2;
        const int alapKarDb = 7;

        const string betuKep = "karakterek\\alapbetu.txt";
        const string szamKep = "karakterek\\alapszam.txt";
        const string kotojelKep = "karakterek\\kotojel.txt";

        static Dictionary<char, char[,]> karakterek = new Dictionary<char, char[,]>();

        public static List<string> Felismer() {
            List<string> rendszamok = new List<string>();

            BeolvasKarakterek(betuKep);
            BeolvasKarakterek(szamKep);
            BeolvasKotojel();

            string[] rendszamFajlok = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"??.txt");

            foreach (string fajl in rendszamFajlok) {
                using(StreamReader sr = new StreamReader(fajl)) {
                    List<string> ls = new List<string>();
                    while (!sr.EndOfStream)
                        ls.Add(sr.ReadLine());
                    char[,] kep = new char[ls.Count, ls[0].Length];
                    for (int i = 0; i < ls.Count; i++) {
                        for (int j = 0; j < ls[0].Length; j++) {
                            kep[i, j] = ls[i][j];
                        }
                    }

                    rendszamok.Add(getNumber(kep));
                }
            }

            return rendszamok;
        }

        private static char getChar(char[,] kep, int bal, int felso, int szel, int mag) {
            foreach (var kar in karakterek) {
                bool jo = true;
                for (int i = felso; i < mag+felso && jo; i++) {
                    for (int j = bal; j < szel+bal && jo; j++) {
                        int y = (i - felso) * alapMagassag / mag;
                        int x = (j - bal) * alapSzelesseg / szel;
                        if (char.ToUpper(kep[i, j]) != char.ToUpper(kar.Value[y, x]))
                            jo = false;
                    }
                }

                if (jo)
                    return kar.Key;
            }

            return ' ';
        }

        private static string getNumber(char[,] kep) {
            int m = kep.GetLength(0);
            int sz = kep.GetLength(1);
            //int koz = 6 * alapKoz;
            //int karSzel = (sz - koz) / alapKarDb;
            int karSzel = m * alapSzelesseg / alapMagassag;
            int koz = (sz-karSzel*7);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < alapKarDb; i++) {
                sb.Append(getChar(kep, (karSzel + koz/6) * i, 0, karSzel, m));
            }

            return sb.ToString();
        }

        private static void BeolvasKarakterek(string file) {
            string str;
            char c = ' ';
            using (StreamReader sr = new StreamReader(file)) {
                while (!sr.EndOfStream && c != 'Z') {
                    c = sr.ReadLine()[0];
                    char[,] kep = new char[alapMagassag,alapSzelesseg];
                    for (int i = 0; i < alapMagassag; i++) {
                        str = sr.ReadLine();
                        for (int j = 0; j < alapSzelesseg; j++) {
                            kep[i, j] = str[j];
                        }
                    }
                    karakterek.Add(c, kep);
                    sr.ReadLine();
                }
            }
        }

        private static void BeolvasKotojel() {
            using (StreamReader sr = new StreamReader(kotojelKep)) {
                char[,] kep = new char[alapMagassag, alapSzelesseg];
                for (int i = 0; i < alapMagassag; i++) {
                    string str = sr.ReadLine();
                    for (int j = 0; j < alapSzelesseg; j++) {
                        kep[i, j] = str[j];
                    }
                }
                karakterek.Add('-', kep);
            }
        }
    }
}
