using System;

namespace AppConsoleDemoCompletoConsole.Models
{
    public abstract class AutoBase
    {
        public abstract void SetAnnoProduzione(int AnnoProduzione);

        public virtual int Prova()
        {
            return 0;
        }
    }
}
