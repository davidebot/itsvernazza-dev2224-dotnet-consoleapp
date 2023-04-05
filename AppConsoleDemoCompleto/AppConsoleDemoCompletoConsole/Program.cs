using AppConsoleDemoCompletoConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppConsoleDemoCompletoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region intro

            // commento
            Console.WriteLine("Hello, World!"); // commento
            /*
             * Multi riga
             *
             superflui
            */

            string name = "Prova";
            var nome1 = "Prova";
            var numero = 1;
            int numeroIntero = 10;
            decimal numeroDecimale = 10.5M;
            numeroIntero = 20;

            const string costante = "A";

            //costante = "B"; impossibile

            int numeroProva;
            numeroProva = 20;

            // var variabile; impossibile

            int varProva;
            varProva = 20;

            string? prova;
            prova = "A";

            bool test = true;

            string nome = "Rocco";
            string cognome = "Zagà";
            //string nomeCompleto = nome + " " + cognome;
            string nomeCompleto = $"{nome} {cognome}";

            int numeroIntero1 = 10;
            decimal numeroDecimale1 = numeroIntero1;

            decimal numeroDecimale2 = 10.546M;
            int numeroIntero2 = Convert.ToInt32(numeroDecimale2);
            int numeroIntero3;
            if (Int32.TryParse(Convert.ToString(numeroDecimale2), out numeroIntero3))
            {
                Console.WriteLine(numeroIntero3);
            }

            decimal senzaDecimali = Math.Round(numeroDecimale2, 0);
            int interoDaDecimal = Convert.ToInt32(senzaDecimali);

            int x = 5;
            int y = 3;
            int somma = x + y; // 8

            int sottrazione = x - y; // 2

            int moltiplicazione = x * y; // 15

            decimal divisione = x / y;

            decimal modulo = x % y;

            x++;
            ++x;
            x += 10;
            x = x + 10;

            string testo = "abcabcabcabc";
            int lunghezza = testo.Length;
            testo.ToUpper();
            testo.ToLower();
            string concatenazione = string.Concat(testo, nome, cognome);
            string concatenazione2 = $"{testo}{nome}{cognome}";

            if (testo.Length > 5)
            {
                char carattere = testo[3];
            }

            List<char> chars = new List<char>();
            foreach(char ch in testo)
            {
                if (ch == 'c')
                {
                    chars.Add(ch);
                }
            }

            int posizione = testo.IndexOf("c");

            string porzione = testo.Substring(2, 4);

            string messaggio = "Ho detto: \\\"\"Ciao\"";

            string messaggio2 = "Ho detto:" + '"' + "Ciao" + '"';

            Console.WriteLine(messaggio);

            #endregion intro

            #region iterazioni
            if (true)
            {

            }
            else if (true)
            { 

            }
            else if (true)
            {

            }
            else
            {

            }

            string valoreSwitch = "Luigi";
            switch (valoreSwitch)
            {
                case "Narcis":
                    Console.WriteLine("Ciao " + valoreSwitch);
                    break;
                case "Matteo":
                    Console.WriteLine("Buongiorno " + valoreSwitch);
                    break;
                default:
                    Console.WriteLine("Salve " + valoreSwitch);
                    break;
            }

            bool esegui = true;
            while (esegui)
            {
                // fa qualcosa
                esegui = false;
            }

            do
            {
                // fa qualcosa
                esegui = false;
            } while (esegui);

            int numeroCaratteri = chars.Count;

            for (int i = 0; i < numeroCaratteri; i++) 
            {
                // fa qualcosa
            }

            foreach (char c in chars)
            {
                if (c == 'b')
                {
                    continue;
                }
                Console.WriteLine(c);
            }

            string[] cars = { "Volvo",  "BMW", "Ford", "Mazda", "Volvo" };
            cars[0] = "Fiat";
            int lunghezzaArray = cars.Length;
            foreach (string car in cars)
            {
                Console.WriteLine(car);
            }

            var autoVolvo = cars.Where(car => car == "Volvo").ToList();

            MetodoNuovo(true);
            MetodoNuovo(false);
            //MetodoNuovo();

            string? numeroDigitato = "1"; // Console.ReadLine();
            if (Int32.TryParse(numeroDigitato, out int numeroFattoriale))
            {
                var risultato = Fattoriale(numeroFattoriale);
                Console.WriteLine("Risultato: " + risultato);
            }

            #endregion iterazioni
            
            try
            {
                Auto autoTest = new Auto("Fiat", "500", 2000, SegmentoAuto.CityCar);
                var autoEpoca = new AutoEpoca("Porsche", "911", 2023, SegmentoAuto.Supercar, "Violetta coi brillantini");
                //Console.WriteLine("Il segmento è:");
                //switch (autoTest.Segmento) 
                //{
                //    case SegmentoAuto.CityCar:
                //        Console.WriteLine("CityCar");
                //        break;
                //    case SegmentoAuto.Berlina:
                //        Console.WriteLine("Berlina");
                //        break;
                //}
            }
            catch (ArgumentException ex) 
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }

            bool continua = false;
            do
            {
                string? marca;
                do
                {
                    Console.WriteLine("Inserire una marca valida:");
                    marca = Console.ReadLine();
                } while (string.IsNullOrEmpty(marca));
                
                Console.WriteLine("Inserire modello");
                string? modello = Console.ReadLine();
                Console.WriteLine("Inserire anno di produzione");
                string? anno = Console.ReadLine();

                Auto auto1 = new Auto();
                if (marca != null)
                {
                    auto1.Marca = marca;
                } 
                auto1.Modello = modello ?? "";
                if (Int32.TryParse(anno, out int annoConvertito))
                {
                    auto1.SetAnnoProduzione(annoConvertito);
                }

                if (string.IsNullOrEmpty(marca))
                {

                }
                if (marca == null || marca == "")
                {

                }

                Console.WriteLine($"La marca inserita è {auto1.Marca}");
                Console.WriteLine($"Il modello inserito è {auto1.Modello}");
                Console.WriteLine($"L'anno di produzione è {auto1.GetAnnoProduzione()}");
                Console.WriteLine($"L'auto è d'epoca: {auto1.IsEpoca()}");

                Console.WriteLine("Vuoi inserire una nuova vettura?");
                string? vuoleContinuare = Console.ReadLine();

                if (vuoleContinuare != null && vuoleContinuare.ToLower() == "sì")
                {
                    continua = true;
                }
                else
                {
                    continua = false;
                }

                if (bool.TryParse(vuoleContinuare, out bool vuoleContinuareCoonvertito))
                {
                    continua = vuoleContinuareCoonvertito;
                } else
                {
                    continua = false;
                }
            } while (continua);

            Console.ReadLine();
        }

        static string MetodoNuovo(bool test2 = false, bool test = true)
        {
            return ""; // MetodoNuovo(false);
        }

        static int Somma(int primo, int secondo)
        {
            return primo + secondo;
        }

        static decimal Somma(decimal primo, decimal secondo)
        {
            return primo + secondo;
        }

        static int Fattoriale(int n)
        {
            int result;
            if (n < 0)
            {
                result = -1;  // situazione anomala
            }
            else if (n == 0)
                result = 1;   // caso base

            else
                result = n * Fattoriale(n - 1); // ricorsione

            return result;
        }
    }
}