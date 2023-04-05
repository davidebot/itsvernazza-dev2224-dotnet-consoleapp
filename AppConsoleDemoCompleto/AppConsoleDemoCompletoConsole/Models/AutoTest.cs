using System;

namespace AppConsoleDemoCompletoConsole.Models
{
    public class AutoTest : AutoEpoca
    {
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
