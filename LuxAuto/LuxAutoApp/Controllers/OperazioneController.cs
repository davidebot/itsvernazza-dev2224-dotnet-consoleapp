using LuxAutoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxAutoApp.Controllers
{
    public class OperazioneController
    {
        private readonly GruppoService _gruppoService;
        private readonly MarchioService _marchioService;
        private readonly VetturaService _vetturaService;

        public OperazioneController(GruppoService gruppoService, MarchioService marchioService, VetturaService vetturaService) 
        {
            _gruppoService = gruppoService;
            _marchioService = marchioService;
            _vetturaService = vetturaService;
        }

        public void SelezionaOperazione()
        {
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
                        Console.WriteLine("GI: Inserimento nuovo gruppo");
                        Console.WriteLine("GM: Modifica gruppo esistente");
                        Console.WriteLine("GC: Cancella gruppo esistente");
                        Console.WriteLine("GV: Visualizza elenco gruppi");
                        Console.WriteLine("MI: Inserimento nuovo marchio");
                        Console.WriteLine("MM: Modifica marchio esistente");
                        Console.WriteLine("MC: Cancella marchio esistente");
                        Console.WriteLine("MV: Visualizza elenco marchi");
                        Console.WriteLine("VI: Inserimento nuova vettura");
                        Console.WriteLine("VM: Modifica vettura esistente");
                        Console.WriteLine("VC: Cancella vettura esistente");
                        Console.WriteLine("VV: Visualizza elenco vetture");
                        Console.WriteLine("VVF: Visualizza elenco vetture filtrato");
                        Console.WriteLine("E: Esci");
                        string? operazione = Console.ReadLine();
                        string[] operazioniConsentite = { "GI", "GM", "GC", "GV", "MI", "MM", "MC", "MV", "VI", "VM", "VC", "VV", "VVF", "E" };

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

                        // se l'utente vuole compiere un'azione, richiama il metodo dell'istanza corrente di VetturaService
                        if (operazioneTerminata == false)
                        { 
                            switch (operazione)
                            {
                                case "GI":
                                    _gruppoService.Inserimento();
                                    break;
                                case "GM":
                                    _gruppoService.Modifica();
                                    break;
                                case "GC":
                                    _gruppoService.Cancellazione();
                                    break;
                                case "GV":
                                    _gruppoService.Visualizzazione();
                                    break;
                                case "MI":
                                    _marchioService.Inserimento();
                                    break;
                                case "MM":
                                    _marchioService.Modifica();
                                    break;
                                case "MC":
                                    _marchioService.Cancellazione();
                                    break;
                                case "MV":
                                    _marchioService.Visualizzazione();
                                    break;
                                case "VI":
                                    _vetturaService.Inserimento();
                                    break;
                                case "VM":
                                    _vetturaService.Modifica();
                                    break;
                                case "VC":
                                    _vetturaService.Cancellazione();
                                    break;
                                case "VV":
                                    _vetturaService.Visualizzazione();
                                    break;
                                case "VVF":
                                    _vetturaService.Ricerca();
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
        }
    }
}
