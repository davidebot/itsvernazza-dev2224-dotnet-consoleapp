using System;

namespace LuxAutoApp.Utilities
{
    public static class GlobalUtilities
    {
        public static bool VuoleContinuare(string messaggio)
        {
            // valore di ritorno
            bool vuoleContinuare = false;

            // variabile per gestire se l'utente ha digitato correttamente il carattere 'S' o 'N'
            bool vuoleContinuareValido = false;

            // ciclo per verificare se l'utente ha digitato correttamente il carattere 'S' o 'N'
            do
            {
                try
                {
                    // input se l'utente vuole continuare 
                    Console.WriteLine(messaggio);
                    Console.WriteLine("(digita S o N)");
                    string? vuoleContinuareInput = Console.ReadLine()?.ToUpper();

                    // controlla che il valore inserito sia valido, altrimenti genera un'eccezione
                    if (vuoleContinuareInput != "S" && vuoleContinuareInput != "N")
                    {
                        throw new ArgumentException("Digita 'S' o 'N'");
                    }
                    else
                    {
                        vuoleContinuareValido = true;
                    }

                    // indica se l'utente vuole continuare
                    if (vuoleContinuareInput == "S")
                    {
                        vuoleContinuare = true;
                    }
                    else
                    {
                        vuoleContinuare = false;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    vuoleContinuareValido = false;
                }
            } while (vuoleContinuareValido == false);

            return vuoleContinuare;
        }
    }
}
