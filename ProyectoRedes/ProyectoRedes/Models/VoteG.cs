namespace ProyectoRedes.Models
{
    public class VoteG
    {
        public string gameId { get; set; }
        public string roundId { get; set; }
        public string password { get; set; }
        public string player { get; set; }

        public bool vote { get; set; }
    }
}
