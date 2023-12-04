using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Snooker
{
    internal class Program
    {

        static List<Verseny> versenyek = new List<Verseny>();
        static void Main(string[] args)
        { 
            string[] olvasottSnooker = File.ReadAllLines("snooker.txt", Encoding.UTF8);
            for (int i = 1; i < olvasottSnooker.Length; i++)
            {
                string[] tagok = olvasottSnooker[i].Split(';');
                Verseny dijazott = new Verseny(Convert.ToInt32(tagok[0]), tagok[1], tagok[2], Convert.ToInt32(tagok[3]));
                versenyek.Add(dijazott);
            }

            Console.WriteLine($"3. feladat: A világranglistán {versenyek.Count} versenyző szerepel");

            double osszegNyeremeny = 0;
            foreach (var verseny in versenyek)
            {
                osszegNyeremeny += verseny.Nyeremeny;
            }

            double atlagFont = osszegNyeremeny / versenyek.Count;

            Console.WriteLine($"4. feladat: A versenyzők átlagosan {atlagFont:N2} fontot kerestek");

           Console.WriteLine("5. feladat: A legjobban kereső kínai versenyző: ");

            var legjobbanKeresoKinai = versenyek
               .Where(x => x.Orszag == "Kína")
               .OrderBy(x => x.Nyeremeny)
               .Last();

            if (legjobbanKeresoKinai != null)
            {                
                double nyeremenyForint = legjobbanKeresoKinai.Nyeremeny * 380;

                Console.WriteLine($"Helyezés: {legjobbanKeresoKinai.Helyezes}");
                Console.WriteLine($"Név: {legjobbanKeresoKinai.Nev}");
                Console.WriteLine($"Ország: {legjobbanKeresoKinai.Orszag}");
                Console.WriteLine($"Nyeremény fontban: {legjobbanKeresoKinai.Nyeremeny}");
                Console.WriteLine($"Nyeremény forintban: {nyeremenyForint}");
            } 
            

            // 6. feladat: Található-e norvég játékos?

            bool talalhato = false;

            foreach(var item in versenyek)
            {
                if( item.Orszag == "Norvégia")
                {
                    talalhato = true;
                    break;                   
                }               
            }  Console.WriteLine("6. feladat: A versenyzők között van norvég versenyző");

            // 7. feladat: Statisztika

            Dictionary<string, int> versenyzokSzamaOrszagonkent = new Dictionary<string, int>();

            foreach (var verseny in versenyek)
            {
                string orszag = verseny.Orszag;

                if (versenyzokSzamaOrszagonkent.ContainsKey(orszag))
                {
                    versenyzokSzamaOrszagonkent[orszag]++;
                }
                else
                {
                    versenyzokSzamaOrszagonkent[orszag] = 1;
                }
            }
          
            Console.WriteLine("\n7. feladat: Statisztika:");
            foreach (var fo in versenyzokSzamaOrszagonkent)
            {
                Console.WriteLine($"\t{fo.Key}: {fo.Value} fő");
            }

            // LINQ-s

            var versenyzokSzamaOrszagonkent2 = versenyek
               .GroupBy(v => v.Orszag)
               .ToDictionary(g => g.Key, g => g.Count());

           
            Console.WriteLine("\nStatisztika2:");
            foreach (var fo2 in versenyzokSzamaOrszagonkent2)
            {
                Console.WriteLine($"\t{fo2.Key}: {fo2.Value} fő");
            }
        }
    }
}
