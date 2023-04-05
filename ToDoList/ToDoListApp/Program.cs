using System;
using System.Linq;
using ToDoListApp.Controllers; 

namespace ToDoListApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // istanza corrente di AttivitaController
            AttivitaController attivitaController = new AttivitaController();

            // variabile per gestire se l'utente vuole compiere un'azione
            bool operazioneTerminata = false;

            // ciclo per gestire se l'utente vuole compiere un'azione
            while (operazioneTerminata == false)
            {
                // variabile per gestire se l'utente ha digitato correttamente il carattere 'S' o 'N'
                bool vuoleFareQualcosaValido = false;
                // ciclo per verificare se l'utente ha digitato correttamente il carattere 'S' o 'N'
                do
                {
                    try
                    {
                        // input dell'azione che vuole compiere l'utente
                        Console.WriteLine("Quale operazione vuoi compiere?");
                        Console.WriteLine("I: Inserimento nuova attività");
                        Console.WriteLine("M: Modifica attività esistente");
                        Console.WriteLine("C: Cancella attività esistente");
                        Console.WriteLine("V: Visualizza elenco attività");
                        Console.WriteLine("E: Esci");
                        string? operazione = Console.ReadLine();
                        string[] operazioniConsentite = { "I", "M", "C", "V", "E" };

                        // controlla che il valore inserito sia valido, altrimenti genera un'eccezione
                        if (operazioniConsentite.Contains(operazione) == false)
                        {
                            throw new ArgumentException("Digita un carattere valido.");
                        }
                        else
                        {
                            vuoleFareQualcosaValido = true;
                        }

                        // se l'utente non vuole compiere un'azione (vuole uscire dal programma), termina il ciclo (quello più esterno)
                        if (operazione == "E")
                        {
                            operazioneTerminata = true;
                        }

                        // se l'utente vuole compiere un'azione, richiama il metodo dell'istanza corrente di AttivitaController
                        if (operazioneTerminata == false)
                        {
                            switch (operazione)
                            {
                                case "I":
                                    attivitaController.Inserimento();
                                    break;
                                case "M":
                                    attivitaController.Modifica();
                                    break;
                                case "C":
                                    attivitaController.Cancellazione();
                                    break;
                                case "V":
                                    attivitaController.Visualizza();
                                    break;
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        vuoleFareQualcosaValido = false;
                    }
                } while (vuoleFareQualcosaValido == false); 
            }

            Console.ReadLine();
        }
    }
}