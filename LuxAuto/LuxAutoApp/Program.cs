using LuxAutoApp.Controllers;
using LuxAutoApp.Exceptions;
using LuxAutoApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;

namespace LuxAutoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // registrazione delle dipendenze
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<GruppoService>()
                    .AddSingleton<MarchioService>()
                    .AddSingleton<VetturaService>()
                    //.AddSingleton<OperazioneController>() // solo per test
                    .BuildServiceProvider();

                // recupero della dipendenza VetturaService registrata
                var vetturaService = serviceProvider.GetRequiredService<VetturaService>();
                // recupero della dipendenza MarchioService registrata
                var marchioService = serviceProvider.GetRequiredService<MarchioService>();
                // recupero della dipendenza GruppoService registrata
                var gruppoService = serviceProvider.GetRequiredService<GruppoService>();

                var operazioneController = new OperazioneController(gruppoService, marchioService, vetturaService);
                operazioneController.SelezionaOperazione();

                // solo per test
                //var operazioneController = serviceProvider.GetRequiredService<OperazioneController>();
                //operazioneController.SelezionaOperazione();
            }
            catch (NapulException ex)
            {
                Console.WriteLine("Guagliò, stà senz pensier");
                Console.WriteLine(ex.Citazione);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore non gestito.");
                Console.WriteLine(ex.Message);
            }


            Console.ReadLine();
        }
    }
}