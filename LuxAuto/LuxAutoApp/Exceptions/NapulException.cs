using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxAutoApp.Exceptions
{
    public class NapulException : System.Exception
    {
        public string Citazione { get; set; } = string.Empty;
        public NapulException(string citazione)
        {
            Citazione = citazione;
        }
    }
}
