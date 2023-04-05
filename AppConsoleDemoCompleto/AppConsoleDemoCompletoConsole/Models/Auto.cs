using System;

namespace AppConsoleDemoCompletoConsole.Models
{
    public class Auto : IAuto //: AutoBase
    {
        public Auto()
        {

        }

        public Auto(string Marca, string Modello, int AnnoProduzione, SegmentoAuto? Segmento = null, string Colore = "")
        {
            this.Marca = Marca;
            this.Modello = Modello;
            this.SetAnnoProduzione(AnnoProduzione);
            this.Segmento = Segmento;
            this.Colore = Colore;
        }

        public SegmentoAuto? Segmento { get; set; } = null;
        public string Colore { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modello { get; set; } = string.Empty;
        protected int AnnoProduzione;

        public virtual void SetAnnoProduzione(int AnnoProduzione)
        {
            if (AnnoProduzione <= 0)
            {
                throw new ArgumentException("L'anno di produzione non è valido", nameof(AnnoProduzione));
            }
            this.AnnoProduzione = AnnoProduzione;
        }

        public int GetAnnoProduzione()
        {
            return this.AnnoProduzione;
        }

        public bool IsEpoca()
        {
            if (this.AnnoProduzione < 1950)
            {
                return true;
            }
            return false;
        }
    }
}
