using System;

namespace LuxAutoApp.Models
{
    public class Marchio
    {
        public Marchio(string nome, Gruppo gruppo)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException(nameof(nome));
            }

            Nome = nome;
            Gruppo = gruppo;
        }

        public string Nome { get; } 

        public Gruppo Gruppo { get; } 
    }
}
