using System;
using System.Text.RegularExpressions;

namespace LuxAutoApp.Models
{
    public class Vettura
    {
        public Vettura(Marchio marca, string modello, uint chilometraggio, string targa)
        { 
            if (string.IsNullOrWhiteSpace(modello))
            {
                throw new ArgumentNullException(nameof(modello));
            }
            if (chilometraggio < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chilometraggio));
            }
            if (string.IsNullOrWhiteSpace(targa))
            {
                throw new ArgumentNullException(nameof(targa));
            }
            targa = targa.Replace(" ", "");
            if (Regex.IsMatch(targa, "[A-Z]{2}[0-9]{3}[A-Z]{2}") == false)
            {
                throw new FormatException("Il formato della targa non è valido.");
            }

            Marca = marca;
            Modello = modello;
            Chilometraggio = chilometraggio;
            Targa = targa;
        }

        public Marchio Marca { get; }

        public string Modello { get; } 

        public uint Chilometraggio { get; } 

        public string Targa { get; } 
    }
}
