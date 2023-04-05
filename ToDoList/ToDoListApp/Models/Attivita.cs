namespace ToDoListApp.Models
{
    public class Attivita
    {
        /// <summary>
        /// Nome dell'attività
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Durata stimata espressa in minuti
        /// </summary>
        public int Durata { get; set; } = 0;

        /// <summary>
        /// Ritorna la durata espressa nel formato HH:mm
        /// </summary>
        /// <returns>Durata stimata in formato HH:mm</returns>
        public string GetDurata()
        {
            int minuti = this.Durata % 60;
            int ore = (this.Durata - minuti) / 60;
            
            return $"{ore}:{minuti}";
        }
    }
}
