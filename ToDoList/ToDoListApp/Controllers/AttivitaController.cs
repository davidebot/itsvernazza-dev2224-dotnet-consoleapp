using System;
using System.Collections.Generic;
using System.Linq; 
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class AttivitaController
    {
        // lista delle attività
        private List<Attivita> todoList = new List<Attivita>();

        public void Inserimento()
        {
            // variabile per gestire se l'utente vuole inserire nuove attività
            bool inserimentoTerminato = false;

            // ciclo per gestire se l'utente vuole inserire nuove attività
            while (inserimentoTerminato == false)
            {
                // input del nome dell'attività
                Console.WriteLine("Inserisci un nome per l'attività");
                string? nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nome),
                        "Il nome dell'attività non è valido.");
                }

                // input della durata stimata dell'attività
                Console.WriteLine("Inserisci la durata stimata dell'attività (in minuti)");
                string? durata = Console.ReadLine();
                if (Int32.TryParse(durata, out int durataConvertita) == false || durataConvertita <= 0) // controlla che la durata sia un numero valido e maggiore di zero
                {
                    throw new InvalidCastException("La durata inserita non è valida.");
                }

                // crea l'istanza della nuova attività che intende inserire l'utente
                Attivita nuovaAttivita = new Attivita()
                {
                    Nome = nome.Trim(),
                    Durata = durataConvertita,
                };

                // controlla se ALMENO UN elemento nella lista delle attività contiene il nome della nuova attività (in minuscolo, senza spazi)
                if (todoList.Any(attivita => attivita.Nome.ToLower().Trim() == nuovaAttivita.Nome.ToLower().Trim()))
                {
                    // eccezione generata se il nome dell'attività è già presente
                    throw new InvalidOperationException($"L'attività con nome {nuovaAttivita.Nome} è già presente");
                }
                else
                {
                    // aggiunge l'attività alla lista
                    todoList.Add(nuovaAttivita);
                }

                // mostra a video l'elenco di tutte le attività
                Visualizza();

                // chiede all'utente se vuole inserire nuove attività
                inserimentoTerminato = VuoleTerminare("Vuoi inserire una nuova attività?");
            }
        }

        public void Modifica()
        {
            // variabile per gestire se l'utente vuole modificare un'attività
            bool modificaTerminata = false;
            // ciclo per gestire se l'utente vuole modificare un'attività
            while (modificaTerminata == false)
            {
                // mostra a video l'elenco di tutte le attività
                Visualizza();

                // input del nome dell'attività da modificare
                Console.WriteLine("Digita il nome dell'attività che vorresti modificare:");
                string? nomeAttivitaDaModificare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeAttivitaDaModificare)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeAttivitaDaModificare),
                        "Il nome dell'attività non è valido.");
                }

                // controlla che il nome sia presente nell'elenco
                if (todoList.Any(x => x.Nome == nomeAttivitaDaModificare) == false)
                {
                    throw new ArgumentException(nameof(nomeAttivitaDaModificare),
                        "Il nome dell'attività non esiste.");
                }

                // input del nuovo nome dell'attività
                Console.WriteLine("Digita il nuovo nome dell'attività:");
                string? nuovoNome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nuovoNome)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nuovoNome),
                        "Il nome dell'attività non è valido.");
                }

                // input della nuova durata stimata dell'attività
                Console.WriteLine("Digita la nuova durata dell'attività:");
                string? nuovaDurata = Console.ReadLine();
                if (Int32.TryParse(nuovaDurata, out int durataConvertita) == false || durataConvertita <= 0) // controlla che la durata sia un numero valido e maggiore di zero
                {
                    throw new InvalidCastException("La durata inserita non è valida.");
                }

                // nuova lista coi valori modificati
                List<Attivita> listaModificata = new List<Attivita>();
                // ciclo per ogni attività nella lista originale
                foreach (var attivita in todoList)
                {
                    // se l'attività corrente è quella che si intende modificare
                    if (attivita.Nome == nomeAttivitaDaModificare)
                    {
                        // crea una nuova attività 
                        var nuovaAttivita = new Attivita()
                        {
                            Nome = nuovoNome,
                            Durata = durataConvertita,
                        };
                        // aggiunge alla nuova lista la nuova attività
                        listaModificata.Add(nuovaAttivita);
                    }
                    else
                    {
                        // aggiunge alla nuova lista l'attività originale
                        listaModificata.Add(attivita);
                    }
                }

                // sostituisce la lista originale con la lista modificata
                todoList = listaModificata;

                // mostra a video l'elenco di tutte le attività
                Visualizza();

                // chiede all'utente se vuole modificare un'altra attività
                modificaTerminata = VuoleTerminare("Vuoi modificare un'altra attività?");
            }
        }

        public void Cancellazione()
        {
            // variabile per gestire se l'utente vuole cancellare un'attività
            bool cancellazioneTerminata = false;

            // ciclo per gestire se l'utente vuole cancellare un'attività
            while (cancellazioneTerminata == false)
            {
                // mostra a video l'elenco di tutte le attività
                Visualizza();

                // input del nome dell'attività da cancellare
                Console.WriteLine("Digita il nome dell'attività che vorresti cancellare:");
                string? nomeAttivitaDaCancellare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeAttivitaDaCancellare)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeAttivitaDaCancellare),
                        "Il nome dell'attività non è valido.");
                }

                // controlla che il nome sia presente nell'elenco
                if (todoList.Any(x => x.Nome == nomeAttivitaDaCancellare) == false)
                {
                    throw new ArgumentException(nameof(nomeAttivitaDaCancellare),
                        "Il nome dell'attività non esiste.");
                }

                // metodo 7 (anche con più nomi uguali) <-- CONSIGLIATO
                // crea una nuova lista di attività filtrata per nome diverso da quello digitato dall'utente e la sostituisce
                todoList = todoList.Where(attivita => attivita.Nome != nomeAttivitaDaCancellare).ToList();

                // mostra a video l'elenco di tutte le attività
                Visualizza();

                // chiede all'utente se vuole cancellare un'altra attività
                cancellazioneTerminata = VuoleTerminare("Vuoi cancellare un'altra attività?");
            }
        }

        public void Visualizza()
        {
            // mostra a video l'elenco di tutte le attività
            Console.WriteLine("Elenco delle attività esistenti:");
            for (int i = 0; i < todoList.Count; i++)
            {
                var numElenco = i + 1;
                var attivita = todoList[i];
                Console.WriteLine($"{numElenco}. Attività: {attivita.Nome}. Durata stimata: {attivita.GetDurata()}");
            }
        }

        private bool VuoleTerminare(string messaggio)
        {
            // valore di ritorno
            bool vuoleTerminare = false;

            // variabile per gestire se l'utente ha digitato correttamente il carattere 'S' o 'N'
            bool vuoleTerminareValido = false;

            // ciclo per verificare se l'utente ha digitato correttamente il carattere 'S' o 'N'
            do
            {
                try
                {
                    // input se l'utente vuole continuare 
                    Console.WriteLine(messaggio);
                    Console.WriteLine("(digita S o N)");
                    string? vuoleTerminareInput = Console.ReadLine()?.ToUpper();

                    // controlla che il valore inserito sia valido, altrimenti genera un'eccezione
                    if (vuoleTerminareInput != "S" && vuoleTerminareInput != "N")
                    {
                        throw new ArgumentException("Digita 'S' o 'N'");
                    }
                    else
                    {
                        vuoleTerminareValido = true;
                    }

                    // indica se l'utente vuole terminare
                    if (vuoleTerminareInput != "S")
                    {
                        vuoleTerminare = true;
                    }
                    else
                    {
                        vuoleTerminare = false;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    vuoleTerminareValido = false;
                }
            } while (vuoleTerminareValido == false);

            return vuoleTerminare;
        }
    }
}
