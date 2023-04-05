using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleDemoCompletoConsole.Models
{
    public interface IAuto
    {
        SegmentoAuto? Segmento { get; set; }
        string Colore { get; set; }
        string Marca { get; set; }
        string Modello { get; set; }
        void SetAnnoProduzione(int AnnoProduzione);
        int GetAnnoProduzione();
        bool IsEpoca();
    }
}
