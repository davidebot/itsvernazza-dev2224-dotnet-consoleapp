using LuxAutoApp.Exceptions;
using LuxAutoApp.Models;
using LuxAutoApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LuxAutoApp.Services
{
    public class VetturaService : ICrudService
    {
        private List<Vettura> vetture = new List<Vettura>();

        // la dipendenza MarchioService registrata
        private readonly MarchioService _marchioService;
        private readonly GruppoService _gruppoService;

        // instanziamento classe VetturaService con recupero della dipendenza MarchioService registrata
        public VetturaService(MarchioService marchioService, GruppoService gruppoService)
        {
            // assegnazione della dipendenza MarchioService registrata
            _marchioService = marchioService;
            _gruppoService = gruppoService;
        }

        public void Inserimento()
        {
            // variabile per gestire se l'utente vuole inserire nuove vetture
            bool continuaInserimento = true;

            // ciclo per gestire se l'utente vuole inserire nuove vetture
            while (continuaInserimento == true)
            {
                // input della marca della vettura
                Console.WriteLine("Inserisci la marca della vettura, scegliendo una tra quelle esistenti");

                // mostra a video l'elenco di tutti i marchi presenti 
                _marchioService.Visualizzazione();

                // recupera lista dei marchi presenti
                List<Marchio> marchiPresenti = _marchioService.GetMarchiPresenti();

                string? marcaInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(marcaInput))
                {
                    throw new ArgumentNullException(nameof(marcaInput));
                }
                var marchioScelto = marchiPresenti.Where(marchio => marchio.Nome == marcaInput).FirstOrDefault();
                if (marchioScelto == null)
                {
                    Console.WriteLine("Marchio non presente. Vuoi inserire il marchio? (digita 'S' o 'N')");
                    string? procedeInserimento = Console.ReadLine();
                    if (procedeInserimento == "S")
                    {
                        _marchioService.Inserimento();
                        marchiPresenti = _marchioService.GetMarchiPresenti();
                        marchioScelto = marchiPresenti.Where(marchio => marchio.Nome == marcaInput).FirstOrDefault(); 
                    }
                    if (marchioScelto == null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(marcaInput), "Il marchio digitato non è stato inserito. Procedere con l'inserimento del marchio prima dell'inserimento della vettura.");
                    }
                }

                // input del modello della vettura
                Console.WriteLine("Inserisci il modello della vettura");
                string? modelloInput = Console.ReadLine();

                // input del chilometraggio della vettura
                Console.WriteLine("Inserisci il chilometraggio della vettura");
                string? chilometraggioInput = Console.ReadLine();
                if (UInt32.TryParse(chilometraggioInput, out uint chilometraggioConvertito) == false) // controlla che il chilometraggio sia un numero valido
                {
                    throw new InvalidCastException("Il chilometraggio inserito non è valido.");
                }

                // input della targa della vettura
                Console.WriteLine("Inserisci la targa della vettura");
                string? targaInput = Console.ReadLine();

                // crea l'istanza della nuova vettura che intende inserire l'utente
                Vettura nuovaVettura = new Vettura(
                    marchioScelto,
                    modelloInput ?? "",
                    chilometraggioConvertito,
                    targaInput ?? ""
                );

                // controlla se ALMENO UN elemento nella lista delle vetture contiene la targa della nuova vettura
                if (vetture.Any(vettura => vettura.Targa == nuovaVettura.Targa))
                {
                    // eccezione generata se la targa della vettura è già presente
                    throw new InvalidOperationException($"La vettura con targa {nuovaVettura.Targa} è già presente");
                }
                else
                {
                    // aggiunge la vettura alla lista
                    vetture.Add(nuovaVettura);
                }

                // mostra a video l'elenco di tutte le vetture
                Visualizzazione();

                // chiede all'utente se vuole continuare l'inserimento di vetture
                continuaInserimento = GlobalUtilities.VuoleContinuare("Vuoi inserire una nuova vettura?");
            }
        }

        public void Modifica()
        {
            // variabile per gestire se l'utente vuole modificare una vettura
            bool continuaModifica = true;

            // ciclo per gestire se l'utente vuole modificare una vettura
            while (continuaModifica == true)
            {
                // mostra a video l'elenco di tutte le vetture
                Visualizzazione();

                // input della targa della vettura da modificare
                Console.WriteLine("Digita la targa della vettura che vorresti modificare:");
                string? targaVetturaDaModificare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(targaVetturaDaModificare)) // controlla che la targa non sia null, vuota o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(targaVetturaDaModificare),
                        "la targa della vettura non è valida.");
                }

                // controlla che la targa sia presente nell'elenco
                if (vetture.Any(x => x.Targa == targaVetturaDaModificare) == false)
                {
                    throw new ArgumentException(nameof(targaVetturaDaModificare),
                        "Non ci sono vetture con la targa inserita.");
                }

                Vettura vetturaDaModificare = vetture.Where(x => x.Targa == targaVetturaDaModificare).First();

                // input della nuova marca della vettura
                Console.WriteLine("Digita la nuova marca della vettura, scegliendo tra i marchi presenti: (invio per ignorare)");

                // mostra a video l'elenco di tutti i marchi presenti 
                _marchioService.Visualizzazione();

                // recupera lista dei marchi presenti
                List<Marchio> marchiPresenti = _marchioService.GetMarchiPresenti();
                string? nuovaMarca = Console.ReadLine();
                Marchio? marchioScelto = vetturaDaModificare.Marca;
                if (string.IsNullOrWhiteSpace(nuovaMarca) == false)
                {
                    marchioScelto = marchiPresenti.Where(marchio => marchio.Nome == nuovaMarca).FirstOrDefault();
                    Console.WriteLine("Marchio non presente. Vuoi inserire il marchio? (digita 'S' o 'N')");
                    string? procedeInserimento = Console.ReadLine();
                    if (procedeInserimento == "S")
                    {
                        _marchioService.Inserimento();
                        marchiPresenti = _marchioService.GetMarchiPresenti();
                        marchioScelto = marchiPresenti.Where(marchio => marchio.Nome == nuovaMarca).FirstOrDefault();
                    }
                }
                if (marchioScelto == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(nuovaMarca), "Il marchio digitato non è stato inserito. Procedere con l'inserimento del marchio prima dell'inserimento della vettura.");
                }

                // input del nuovo modello della vettura
                Console.WriteLine("Digita il nuovo modello della vettura: (invio per ignorare)");
                string? nuovoModello = Console.ReadLine();
                if (string.IsNullOrEmpty(nuovoModello))
                {
                    nuovoModello = vetturaDaModificare.Modello;
                }

                // input del chilometraggio della vettura
                Console.WriteLine("Digita il nuovo chilometraggio della vettura: (invio per ignorare)");
                string? nuovoChilometraggioInput = Console.ReadLine();
                uint nuovoChilometraggio = vetturaDaModificare.Chilometraggio;
                if (string.IsNullOrEmpty(nuovoChilometraggioInput) == false)
                {
                    if (UInt32.TryParse(nuovoChilometraggioInput, out nuovoChilometraggio) == false || nuovoChilometraggio < vetturaDaModificare.Chilometraggio)
                    {
                        throw new NapulException("'O chilometragg' nun se sceglie... 'e cumpagne sì");
                    }
                }

                // nuova lista coi valori modificati
                List<Vettura> listaModificata = new List<Vettura>();

                // ciclo per ogni vettura nella lista originale
                foreach (Vettura vettura in vetture)
                {
                    // se la vettura corrente è quella che si intende modificare
                    if (vettura.Targa == targaVetturaDaModificare)
                    {
                        // crea una nuova vettura 
                        var nuovaVettura = new Vettura(marchioScelto, nuovoModello, nuovoChilometraggio, vettura.Targa);
                        // aggiunge alla nuova lista la nuova vettura
                        listaModificata.Add(nuovaVettura);
                    }
                    else
                    {
                        // aggiunge alla nuova lista la vettura originale
                        listaModificata.Add(vettura);
                    }
                }

                // sostituisce la lista originale con la lista modificata
                vetture = listaModificata;

                // mostra a video l'elenco di tutte le vetture
                Visualizzazione();

                // chiede all'utente se vuole modificare un'altra vettura
                continuaModifica = GlobalUtilities.VuoleContinuare("Vuoi modificare un'altra vettura?");
            }
        }

        public void Cancellazione()
        {
            // variabile per gestire se l'utente vuole cancellare una vettura
            bool continuaCancellazione = true;

            // ciclo per gestire se l'utente vuole cancellare una vettura
            while (continuaCancellazione == true)
            {
                // mostra a video l'elenco di tutte le vetture
                Visualizzazione();

                // input della targe della vettura da cancellare
                Console.WriteLine("Digita la targa della vettura che vorresti cancellare:");
                string? targaVetturaDaCancellare = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(targaVetturaDaCancellare)) // controlla che la targa non sia null, vuoto o contenga soltanto spazi
                {
                    throw new ArgumentNullException(nameof(targaVetturaDaCancellare),
                        "La targa della vettura non è valida.");
                }

                // controlla che la targa sia presente nell'elenco
                if (vetture.Any(x => x.Targa == targaVetturaDaCancellare) == false)
                {
                    throw new ArgumentException(nameof(targaVetturaDaCancellare),
                        "Non ci sono vetture con la targa inserita.");
                }

                // crea una nuova lista di vetture filtrata per targa diversa da quello digitata dall'utente e la sostituisce
                vetture = vetture.Where(vettura => vettura.Targa != targaVetturaDaCancellare).ToList();

                // mostra a video l'elenco di tutte le vetture
                Visualizzazione();

                // chiede all'utente se vuole cancellare un'altra vettura
                continuaCancellazione = GlobalUtilities.VuoleContinuare("Vuoi cancellare un'altra vettura?");
            }
        }

        public void Visualizzazione()
        {
            // mostra a video l'elenco di tutte le vetture
            Console.WriteLine("Elenco delle vetture presenti:");
            for (int i = 0; i < vetture.Count; i++)
            {
                var numElenco = i + 1;
                var vettura = vetture[i];
                Console.WriteLine($"{numElenco}. Targa: {vettura.Targa}. Marca: {vettura.Marca.Nome}. Gruppo: {vettura.Marca.Gruppo.Nome}. Modello: {vettura.Modello}. Chilometraggio: {vettura.Chilometraggio}.");
            }
        }

        public void Ricerca()
        {
            Console.WriteLine("Digita una targa o premi invio per ignorare.");
            string? targaInput = Console.ReadLine();
            Console.WriteLine("Digita una marca o premi invio per ignorare.");
            _marchioService.Visualizzazione();
            string? marcaInput = Console.ReadLine();
            Console.WriteLine("Digita un gruppo o premi invio per ignorare.");
            _gruppoService.Visualizzazione();
            string? gruppoInput = Console.ReadLine();
            Console.WriteLine("Digita un modello o premi invio per ignorare.");
            string? modelloInput = Console.ReadLine();
            Console.WriteLine("Digita il chilometraggio minimo o premi invio per ignorare.");
            string? chilometraggioMinInput = Console.ReadLine();
            Console.WriteLine("Digita il chilometraggio massimo o premi invio per ignorare.");
            string? chilometraggioMaxInput = Console.ReadLine();

            uint? chilometraggioMin = null;
            uint? chilometraggioMax = null;

            // mostra a video l'elenco di tutte le vetture di un dato marchio
            var messaggio = "Elenco delle vetture filtrato";
            var filtri = "";
            if (string.IsNullOrEmpty(targaInput) == false)
            {
                filtri += $" targa '{targaInput}'";
            }
            if (string.IsNullOrEmpty(marcaInput) == false)
            {
                if (string.IsNullOrEmpty(filtri) == false)
                {
                    filtri += ",";
                }
                filtri += $" marchio '{marcaInput}'";
            }
            if (string.IsNullOrEmpty(gruppoInput) == false)
            {
                if (string.IsNullOrEmpty(filtri) == false)
                {
                    filtri += ",";
                }
                filtri += $" gruppo '{gruppoInput}'";
            }
            if (string.IsNullOrEmpty(modelloInput) == false)
            {
                if (string.IsNullOrEmpty(filtri) == false)
                {
                    filtri += ",";
                }
                filtri += $" modello '{modelloInput}'";
            }
            if (string.IsNullOrEmpty(chilometraggioMinInput) == false)
            {
                if (UInt32.TryParse(chilometraggioMinInput, out uint chilometraggioMinConvertito))
                {
                    chilometraggioMin = chilometraggioMinConvertito;
                }
                else
                {
                    throw new InvalidCastException("Il chilometraggio inserito non è valido.");
                }

                if (string.IsNullOrEmpty(filtri) == false)
                {
                    filtri += ",";
                }
                filtri += $" chilometraggio minimo '{chilometraggioMinInput}'";
            }
            if (string.IsNullOrEmpty(chilometraggioMaxInput) == false)
            {
                if (UInt32.TryParse(chilometraggioMaxInput, out uint chilometraggioMaxConvertito))
                {
                    chilometraggioMax = chilometraggioMaxConvertito;
                }
                else
                {
                    throw new InvalidCastException("Il chilometraggio inserito non è valido.");
                }

                if (string.IsNullOrEmpty(filtri) == false)
                {
                    filtri += ",";
                }
                filtri += $" chilometraggio massimo '{chilometraggioMaxInput}'";
            }
            messaggio += filtri + ":";

            Console.WriteLine(messaggio);
            var listaFiltrata = vetture.Where(vettura => 
                (string.IsNullOrEmpty(targaInput) || vettura.Targa == targaInput) &&
                (string.IsNullOrEmpty(marcaInput) || vettura.Marca.Nome == marcaInput) &&
                (string.IsNullOrEmpty(gruppoInput) || vettura.Marca.Gruppo.Nome == gruppoInput) && 
                (string.IsNullOrEmpty(modelloInput) || vettura.Modello == modelloInput) && 
                (chilometraggioMin == null || vettura.Chilometraggio >= chilometraggioMin) &&
                (chilometraggioMax == null || vettura.Chilometraggio <= chilometraggioMax) 
            ).ToList();
            for (int i = 0; i < listaFiltrata.Count; i++)
            {
                var numElenco = i + 1;
                var vettura = listaFiltrata[i];
                Console.WriteLine($"{numElenco}. Targa: {vettura.Targa}. Marca: {vettura.Marca.Nome}. Gruppo: {vettura.Marca.Gruppo.Nome}. Modello: {vettura.Modello}. Chilometraggio: {vettura.Chilometraggio}.");
            }
        }
    }
}
