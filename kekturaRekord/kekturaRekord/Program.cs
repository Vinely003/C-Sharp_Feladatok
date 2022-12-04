using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

//1. feladat- Készítsen programot a következő feladatok megoldására, amelynek a forráskódját kektura néven mentse el!
namespace kekturaRekord
{
    //2. feladat - Olvassa be a kektura.csv állományban lévő adatokat és tárolja el úgy, hogy a további feladatok megoldására alkalmas legyen! 
    //A fájlban legfeljebb 100 sor lehet!
    class Utvonal
    {
        public string KiinduloPont;
        public string VegPont;
        public double Hossz;
        public int Emelkedes;
        public int Lejtes;
        public char PecseteloHely;

        public Utvonal(string KiinduloPont, string VegPont, double Hossz, int Emelkedes, int Lejtes, char PecseteloHely)
        {
            this.KiinduloPont = KiinduloPont;
            this.VegPont = VegPont;
            this.Hossz = Hossz;
            this.Emelkedes = Emelkedes;
            this.Lejtes = Lejtes;
            this.PecseteloHely = PecseteloHely;
        }
    }
    class Program
    {
        //6. feladat - Készítsen logikai értékkel visszatérő függvényt (vagy jellemzőt) HianyosNev azonosítóval, melynek segítségével minősíteni tudja a túraszakaszok végpontjainak a nevét! 
        //Hiányos állomásneveknek minősítjük azokat a végpontneveket, melyek pecsételőhelyek, de a „pecsetelohely” karakterlánc nem található meg a nevükben. 
        //Ebben az esetben logikai igaz értékkel térjen vissza a függvény (vagy jellemző), egyébként pedig hamissal.
        static bool HianyosNev(string VizsgalandoNev)
        {
            //bool NemHianyos=false;
            if (VizsgalandoNev.Contains("pecsetelohely"))
            {
                return true;
                //NemHianyos=true;
            }
            else 
            {
                return false;
                //NemHianyos=false;
            }
            //return NemHianyos;
        }
        static void Main(string[] args)
        {
            StreamReader Olvas = new StreamReader("kektura.csv", Encoding.Default);
            List<Utvonal> Tura = new List<Utvonal>();
            int KezdoMagassag = Convert.ToInt32(Olvas.ReadLine());
            while (!Olvas.EndOfStream)
            {
                string Sor = Olvas.ReadLine();
                string[] Sorelemek = Sor.Split(';');
                Tura.Add(new Utvonal(Sorelemek[0], Sorelemek[1], Convert.ToDouble(Sorelemek[2]), Convert.ToInt32(Sorelemek[3]), Convert.ToInt32(Sorelemek[4]), Convert.ToChar(Sorelemek[5])));
            }
            Olvas.Close();
            //3. feladat - Határozza meg és írja ki a képernyőre a minta szerint, hogy hány szakasz található a kektura.csv állományban!
            Console.WriteLine($"3. feladat: Szakaszok száma {Tura.Count} db");
            //4. feladat - Határozza meg és írja ki a képernyőre a minta szerint, a túra teljes hosszát!
            double TeljesHossz = 0;
            for (int i = 0; i < Tura.Count; i++)
            {
                TeljesHossz += Tura[i].Hossz;
            }
            Console.WriteLine($"A túra teljes hossza: {TeljesHossz} km");
            //5. feladat - Keresse meg és írja ki a képernyőre a túra legrövidebb szakaszának adatait a minta szerint!
            //Feltételezheti, hogy nincs két egyforma hosszúságú szakasz!
            int LegrovidebbIndex = 0;
            for (int i = 1; i < Tura.Count; i++)
            {
                if (Tura[i].Hossz < Tura[LegrovidebbIndex].Hossz)
                {
                    LegrovidebbIndex = i;                
                }
            }
            Console.WriteLine("5. feladat: A legrövidebb szakasz adatai:");
            Console.WriteLine($"\tKezdete: {Tura[LegrovidebbIndex].KiinduloPont}");
            Console.WriteLine($"\tVége: {Tura[LegrovidebbIndex].VegPont}");
            Console.WriteLine($"\tTávolság: {Tura[LegrovidebbIndex].Hossz}");
            //7. feladat - Keresse meg és írja ki a minta szerint a képernyőre a hiányos állomásneveket! 
            //Ha nincs hiányos állomásnév az adatokban, akkor a „Nincs hiányos állomásnév!” felirat jelenjen meg!
            Console.WriteLine("7. feladat: Hiányos állomásnevek:");
            for (int i = 0; i < Tura.Count; i++)
            {
                if (Tura[i].PecseteloHely == 'i' && HianyosNev(Tura[i].VegPont) == false)
                {
                    Console.WriteLine($"\t{Tura[i].VegPont}");
                }
            }
            //8. feladat - Ismerjük a túra kiindulópontjának tengerszint feletti magasságát és az egyes túraszakaszokon mért emelkedések és lejtések összegét. 
            //Az adatok ismeretében keressük meg a túra legmagasabban fekvő végpontját és határozzuk meg a végpont tengerszint feletti magasságát! 
            //Feltételezheti, hogy nincs kettő vagy több ilyen végpont!
            int LegmagasabbIndexe = 0;
            int LegMagasabb = KezdoMagassag;
            int AktMagassag = KezdoMagassag;
            for (int i = 0; i < Tura.Count; i++)
            { 
                AktMagassag+= (Tura[i].Emelkedes - Tura[i].Lejtes);
                if (AktMagassag > LegMagasabb)
                {
                    LegMagasabb = AktMagassag;
                    LegmagasabbIndexe = i;
                }
            }
            Console.WriteLine("8. feladat: A túra legmagasabb fekvő végpontja:");
            Console.WriteLine("\tA végpont neve:"+Tura[LegmagasabbIndexe].VegPont);
            Console.WriteLine($"\tA végpont tengerszint feletti magassága: {LegMagasabb} m");
            //9. feladat - Készítsen kektura2.csv néven szöveges állományt, mely szerkezete megegyezik a kektura.csv állományéval! 
            //A kimeneti fájl első sora a kiindulópont tengerszint feletti magasságát tartalmazza! 
            //A további sorokban a túra szakaszainak adatait írja ki! Azoknál a pecsételőhelyeknél, ahol nem található meg a végpont nevében a „pecsetelohely” 
            //karaktersorozat, ott kerüljön be a végpont nevének a végére egy szóközzel elválasztva a „pecsetelohely” szó!
            StreamWriter Iro = new StreamWriter("kektura2.csv", false, Encoding.Default);
            Iro.WriteLine(KezdoMagassag);
            for (int i = 0; i < Tura.Count; i++)
            {
                Iro.Write(Tura[i].KiinduloPont + ";");
                if (HianyosNev(Tura[i].VegPont) == false && Tura[i].PecseteloHely == 'i')
                {
                    Iro.Write(Tura[i].VegPont + " pecsetelohely;");

                }
                else
                {
                    Iro.Write(Tura[i].VegPont + ";");
                }
                Iro.WriteLine($"{Tura[i].Hossz};{Tura[i].Emelkedes};{Tura[i].Lejtes};{Tura[i].PecseteloHely}");
            }
            Iro.Close();

            Console.ReadLine();
        }
    }
}
