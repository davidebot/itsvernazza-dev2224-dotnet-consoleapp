using LuxAutoApp.Models;
using LuxAutoApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuxAutoApp.Services
{
    public class MarchioService : ICrudService
    {
        private List<Marchio> marchi = new List<Marchio>();

        private readonly GruppoService _gruppoService;

        public MarchioService(GruppoService gruppoService) 
        {
            _gruppoService = gruppoService;
        }

        public List<Marchio> GetMarchiPresenti() 
        { 
            return marchi; 
        }

        public void Inserimento()
        {
            // variabile per gestire se l'utente vuole inserire nuovi marchi
            bool continuaInserimento = true;

            // ciclo per gestire se l'utente vuole inserire nuovi marchi
            while (continuaInserimento == true)
            {
                // input del nome del marchio
                Console.WriteLine("Inserisci il nome del marchio");
                string? nomeInput = Console.ReadLine();

                // input del gruppo del marchio
                Console.WriteLine("Inserisci il gruppo del marchio, scegliendo uno tra quelli esistenti");

                // mostra a video l'elenco di tutti i gruppi presenti 
                _gruppoService.Visualizzazione();

                // recupera lista dei gruppi presenti
                List<Gruppo> gruppiPresenti = _gruppoService.GetGruppiPresenti();

                string? gruppoInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(gruppoInput))
                {
                    throw new ArgumentNullException(nameof(gruppoInput));
                }
                var gruppoScelto = gruppiPresenti.Where(gruppo => gruppo.Nome == gruppoInput).FirstOrDefault();
                if (gruppoScelto == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(gruppoInput), "Il gruppo digitato non è stato inserito. Procedere con l'inserimento del gruppo prima dell'inserimento del marchio.");
                }

                // crea l'istanza del nuovo marchio che intende inserire l'utente
                Marchio nuovoMarchio = new Marchio(
                    nomeInput ?? "",
                    gruppoScelto
                );

                // controlla se ALMENO UN elemento nella lista dei marchi contiene il nome del nuovo marchio
                if (marchi.Any(marchio => marchio.Nome == nuovoMarchio.Nome))
                {
                    // eccezione generata se il nome del marchio è già presente
                    throw new InvalidOperationException($"Il marchio con nome  {nuovoMarchio.Nome} è già presente");
                }
                else
                {
                    // aggiunge il marchio alla lista
                    marchi.Add(nuovoMarchio);
                }

                // mostra a video l'elenco di tutti i marchi
                Visualizzazione();

                // chiede all'utente se vuole continuare l'inserimento di marchi
                continuaInserimento = GlobalUtilities.VuoleContinuare("Vuoi inserire un nuovo marchio?");
            }
        }

        public void Modifica()
        {
            // variabile per gestire se l'utente vuole modificare un marchio
            bool continuaModifica = true;

            // ciclo per gestire se l'utente vuole modificare un marchio
            while (continuaModifica == true)
            {
                // mostra a video l'elenco di tutti i marchi
                Visualizzazione();

                // input del nome del marchio da modificare
                Console.WriteLine("Digita il nome del marchio che vorresti modificare:");
                string? nomeMarchioDaModificare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeMarchioDaModificare)) // controlla che il nome non sia null, vuota o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeMarchioDaModificare),
                        "Il nome del marchio non è valido.");
                }

                // controlla che il marchio sia presente nell'elenco
                if (marchi.Any(x => x.Nome == nomeMarchioDaModificare) == false)
                {
                    throw new ArgumentException(nameof(nomeMarchioDaModificare),
                        "Non ci sono marchi con il nome inserito.");
                }

                Marchio marchioDaModificare = marchi.Where(x => x.Nome == nomeMarchioDaModificare).First();

                // input del nuovo nome del marchio
                Console.WriteLine("Digita il nuovo nome del marchio: (invio per ignorare)");
                string? nuovoNome = Console.ReadLine();
                if (string.IsNullOrEmpty(nuovoNome))
                {
                    nuovoNome = marchioDaModificare.Nome;
                }

                // input del nuovo gruppo del marchio
                Console.WriteLine("Digita il nuovo gruppo del marchio, scegliendo tra i gruppi presenti: (invio per ignorare)");

                // mostra a video l'elenco di tutti i gruppi presenti 
                _gruppoService.Visualizzazione();

                // recupera lista dei gruppi presenti
                List<Gruppo> gruppiPresenti = _gruppoService.GetGruppiPresenti();

                string? nuovoGruppo = Console.ReadLine();
                Gruppo? gruppoScelto = marchioDaModificare.Gruppo;
                if (string.IsNullOrWhiteSpace(nuovoGruppo) == false)
                {
                    gruppoScelto = gruppiPresenti.Where(gruppo => gruppo.Nome == nuovoGruppo).FirstOrDefault();
                }
                if (gruppoScelto == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(nuovoGruppo), "Il gruppo digitato non è stato inserito. Procedere con l'inserimento del gruppo prima dell'inserimento del marchio.");
                }

                // nuova lista coi valori modificati
                List<Marchio> listaModificata = new List<Marchio>();

                // ciclo per ogni marchio nella lista originale
                foreach (Marchio marchio in marchi)
                {
                    // se il marchio corrente è quello che si intende modificare
                    if (marchio.Nome == nomeMarchioDaModificare)
                    {
                        // crea un nuovo marchio
                        var nuovoMarchio = new Marchio(nuovoNome, gruppoScelto);
                        // aggiunge alla nuova lista il nuovo marchio
                        listaModificata.Add(nuovoMarchio);
                    }
                    else
                    {
                        // aggiunge alla nuova lista il marchio originale
                        listaModificata.Add(marchio);
                    }
                }

                // sostituisce la lista originale con la lista modificata
                marchi = listaModificata;

                // mostra a video l'elenco di tutti i marchi
                Visualizzazione();

                // chiede all'utente se vuole modificare un altro marchio
                continuaModifica = GlobalUtilities.VuoleContinuare("Vuoi modificare un altro marchio?");
            }
        }

        public void Cancellazione()
        {
            // variabile per gestire se l'utente vuole cancellare un marchio
            bool continuaCancellazione = true;

            // ciclo per gestire se l'utente vuole cancellare un marchio
            while (continuaCancellazione == true)
            {
                // mostra a video l'elenco di tutti i marchi
                Visualizzazione();

                // input del nome del marchio da cancellare
                Console.WriteLine("Digita il nome del marchio che vorresti cancellare:");
                string? nomeMarchioDaCancellare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeMarchioDaCancellare)) // controlla che il nome non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(nomeMarchioDaCancellare),
                        "Il nome del marchio non è valido.");
                }

                // controlla che il marchio sia presente nell'elenco
                if (marchi.Any(x => x.Nome == nomeMarchioDaCancellare) == false)
                {
                    throw new ArgumentException(nameof(nomeMarchioDaCancellare),
                        "Non ci sono marchi con il nome inserito.");
                }

                // crea una nuova lista di marchi filtrata per nome diverso da quello digitato dall'utente e la sostituisce
                marchi = marchi.Where(marchio => marchio.Nome != nomeMarchioDaCancellare).ToList();

                // mostra a video l'elenco di tutti i marchi
                Visualizzazione();

                // chiede all'utente se vuole cancellare un altro marchio
                continuaCancellazione = GlobalUtilities.VuoleContinuare("Vuoi cancellare un altro marchio?");
            }
        }

        public void Visualizzazione()
        {
            // mostra a video l'elenco di tutti i marchi
            Console.WriteLine("Elenco dei marchi presenti:");
            for (int i = 0; i < marchi.Count; i++)
            {
                var numElenco = i + 1;
                var marchio = marchi[i];
                Console.WriteLine($"{numElenco}. Nome: {marchio.Nome}. Gruppo: {marchio.Gruppo.Nome}.");
            }
        }
    }
}
