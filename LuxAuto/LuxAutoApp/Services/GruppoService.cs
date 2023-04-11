using LuxAutoApp.Models;
using LuxAutoApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxAutoApp.Services
{
    public class GruppoService : ICrudService
    {
        private List<Gruppo> gruppi = new List<Gruppo>();

        public GruppoService() 
        {
            
        }

        public List<Gruppo> GetGruppiPresenti() 
        { 
            return gruppi; 
        }

        public void Inserimento()
        {
            // variabile per gestire se l'utente vuole inserire nuovi gruppi
            bool continuaInserimento = true;

            // ciclo per gestire se l'utente vuole inserire nuovi gruppi
            while (continuaInserimento == true)
            {
                // input del nome del gruppo
                Console.WriteLine("Inserisci il nome del gruppo");
                string? nomeInput = Console.ReadLine();

                // input della nazionalità del gruppo 
                Console.WriteLine("Inserisci la nazionalità del gruppo");
                string? nazionalitaInput = Console.ReadLine();

                // crea l'istanza del nuovo gruppo che intende inserire l'utente
                Gruppo nuovoGruppo = new Gruppo(
                    nomeInput ?? "",
                    nazionalitaInput ?? ""
                );

                // controlla se ALMENO UN elemento nella lista dei gruppi contiene il nome del nuovo gruppo
                if (gruppi.Any(gruppo => gruppo.Nome == nuovoGruppo.Nome))
                {
                    // eccezione generata se il nome del gruppo è già presente
                    throw new InvalidOperationException($"Il gruppo con nome  {nuovoGruppo.Nome} è già presente");
                }
                else
                {
                    // aggiunge il gruppo alla lista
                    gruppi.Add(nuovoGruppo);
                }

                // mostra a video l'elenco di tutti i gruppi
                Visualizzazione();

                // chiede all'utente se vuole continuare l'inserimento di gruppi
                continuaInserimento = GlobalUtilities.VuoleContinuare("Vuoi inserire un nuovo gruppo?");
            }
        }

        public void Modifica()
        {
            // variabile per gestire se l'utente vuole modificare un gruppo
            bool continuaModifica = true;

            // ciclo per gestire se l'utente vuole modificare un gruppo
            while (continuaModifica == true)
            {
                // mostra a video l'elenco di tutti i gruppi
                Visualizzazione();

                // input del nome del gruppo da modificare
                Console.WriteLine("Digita il nome del gruppo che vorresti modificare:");
                string? nomeGruppoDaModificare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeGruppoDaModificare)) // controlla che il nome non sia null, vuota o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeGruppoDaModificare),
                        "Il nome del gruppo non è valido.");
                }

                // controlla che il gruppo sia presente nell'elenco
                if (gruppi.Any(x => x.Nome == nomeGruppoDaModificare) == false)
                {
                    throw new ArgumentException(nameof(nomeGruppoDaModificare),
                        "Non ci sono gruppi con il nome inserito.");
                }

                Gruppo gruppoDaModificare = gruppi.Where(x => x.Nome == nomeGruppoDaModificare).First();

                // input del nuovo nome del gruppo
                Console.WriteLine("Digita il nuovo nome del gruppo: (invio per ignorare)");
                string? nuovoNome = Console.ReadLine();
                if (string.IsNullOrEmpty(nuovoNome))
                {
                    nuovoNome = gruppoDaModificare.Nome;
                }

                // input della nuova nazionalità del gruppo
                Console.WriteLine("Digita la nuova nazionalità del gruppo: (invio per ignorare)");
                string? nuovaNazionalita = Console.ReadLine();
                if (string.IsNullOrEmpty(nuovaNazionalita))
                {
                    nuovaNazionalita = gruppoDaModificare.Nazionalita;
                }

                // nuova lista coi valori modificati
                List<Gruppo> listaModificata = new List<Gruppo>();

                // ciclo per ogni gruppo nella lista originale
                foreach (Gruppo gruppo in gruppi)
                {
                    // se il gruppo corrente è quello che si intende modificare
                    if (gruppo.Nome == nomeGruppoDaModificare)
                    {
                        // crea un nuovo gruppo
                        var nuovoGruppo = new Gruppo(nuovoNome, nuovaNazionalita);
                        // aggiunge alla nuova lista il nuovo gruppo
                        listaModificata.Add(nuovoGruppo);
                    }
                    else
                    {
                        // aggiunge alla nuova lista il gruppo originale
                        listaModificata.Add(gruppo);
                    }
                }

                // sostituisce la lista originale con la lista modificata
                gruppi = listaModificata;

                // mostra a video l'elenco di tutti i gruppi
                Visualizzazione();

                // chiede all'utente se vuole modificare un altro gruppo
                continuaModifica = GlobalUtilities.VuoleContinuare("Vuoi modificare un altro gruppo?");
            }
        }

        public void Cancellazione()
        {
            // variabile per gestire se l'utente vuole cancellare un gruppo
            bool continuaCancellazione = true;

            // ciclo per gestire se l'utente vuole cancellare un gruppo
            while (continuaCancellazione == true)
            {
                // mostra a video l'elenco di tutti i gruppi
                Visualizzazione();

                // input del nome del gruppo da cancellare
                Console.WriteLine("Digita il nome del gruppo che vorresti cancellare:");
                string? nomeGruppoDaCancellare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeGruppoDaCancellare)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeGruppoDaCancellare),
                        "Il nome del gruppo non è valido.");
                }

                // controlla che il gruppo sia presente nell'elenco
                if (gruppi.Any(x => x.Nome == nomeGruppoDaCancellare) == false)
                {
                    throw new ArgumentException(nameof(nomeGruppoDaCancellare),
                        "Non ci sono gruppi con il nome inserito.");
                }

                // crea una nuova lista di gruppi filtrata per nome diverso da quello digitato dall'utente e la sostituisce
                gruppi = gruppi.Where(gruppo => gruppo.Nome != nomeGruppoDaCancellare).ToList();

                // mostra a video l'elenco di tutti i gruppi
                Visualizzazione();

                // chiede all'utente se vuole cancellare un altro gruppo
                continuaCancellazione = GlobalUtilities.VuoleContinuare("Vuoi cancellare un altro gruppo?");
            }
        }

        public void Visualizzazione()
        {
            // mostra a video l'elenco di tutti i gruppi
            Console.WriteLine("Elenco dei gruppi presenti:");
            for (int i = 0; i < gruppi.Count; i++)
            {
                var numElenco = i + 1;
                var gruppo = gruppi[i];
                Console.WriteLine($"{numElenco}. Nome: {gruppo.Nome}. Nazionalità: {gruppo.Nazionalita}.");
            }
        }
    }
}
