using System;

namespace AppConsoleDemoCompletoConsole.Models
{
    public class AutoEpoca : Auto, IAutoEpoca
    {
        public AutoEpoca() : base()
        {
        }

        public AutoEpoca(
            string Marca, 
            string Modello, 
            int AnnoProduzione,
            SegmentoAuto? Segmento = null, 
            string Colore = "") 
            : base(Marca, Modello, AnnoProduzione, Segmento, Colore)
        {

        }

        public bool IsRottamata { get; set; } = false;

        public override void SetAnnoProduzione (int AnnoProduzione)
        {
            if (AnnoProduzione <= 0 || AnnoProduzione >= 1950)
            {
                throw new ArgumentException("L'anno di produzione non è valido", nameof(AnnoProduzione));
            }
            this.AnnoProduzione = AnnoProduzione;
        }
    }
}
