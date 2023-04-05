using System.Data;
using System.Text.Json;

namespace ThrowExceptions101
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int numMinore = 0;
                int numMaggiore = 0;
                bool inputValido = false;

                do
                {
                    try
                    {
                        Console.WriteLine("Inserire il numero minore:");
                        string? numMinoreInput = Console.ReadLine();
                        if (Int32.TryParse(numMinoreInput, out numMinore) == false)
                        {
                            throw new InvalidCastException(
                                "Il numero minore non è valido.");
                        }

                        Console.WriteLine("Inserire il numero maggiore:");
                        string? numMaggioreInput = Console.ReadLine();
                        if (Int32.TryParse(numMaggioreInput, out numMaggiore) == false)
                        {
                            throw new InvalidCastException(
                                "Il numero maggiore non è valido.");
                        }

                        // Inserimento OK
                        inputValido = true;
                    }
                    catch (InvalidCastException ex)
                    {
                        inputValido = false;
                        Console.WriteLine(ex.Message);
                    }
                } while (inputValido == false);

                try
                {
                    decimal mediaNumeriPari = MediaNumeriPari(numMinore, numMaggiore);

                    Console.WriteLine($"La media dei numeri pari contenuti tra {numMinore} e {numMaggiore} è {mediaNumeriPari}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Si è verificato un errore.");
                    Console.WriteLine(ex.Message);
                }
                catch (Exception) 
                {
                    Console.WriteLine("Si è verificato un errore inaspettato nel calcolo della media.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore inaspettato.");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        static decimal MediaNumeriPari(int numeroMinore, int numeroMaggiore)
        {
            if (numeroMinore >= numeroMaggiore)
            {
                throw new ArgumentOutOfRangeException(nameof(numeroMinore),
                    "Il numero minore non può essere maggiore o uguale del numero maggiore");
            }

            int somma = 0;
            int contatore = 0;
            decimal media = 0;

            for (int i = numeroMinore; i <= numeroMaggiore; i++)
            {
                if (i % 2 == 0)
                {
                    somma += i;
                    contatore++;
                }
            }

            media = (decimal)somma / contatore;

            return media;
        }
    }
}