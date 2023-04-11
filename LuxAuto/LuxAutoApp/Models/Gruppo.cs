using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxAutoApp.Models
{
    public class Gruppo
    {
        public Gruppo(string nome, string nazionalita)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException(nameof(nome));
            }
            if (string.IsNullOrWhiteSpace(nazionalita))
            {
                throw new ArgumentNullException(nameof(nazionalita));
            }

            Nome = nome;
            Nazionalita = nazionalita;
        }

        public string Nome { get; set; } = string.Empty;
        public string Nazionalita { get; set; }
    }
}
