using System.Security.Policy;

namespace ProyectoRedes.Models
{
    public class Player
    {

        public List<PlayerCheck> otherPlayers { get; set; }

        public string player { get;set; }

        public string leader { get; set; }

        public List<bool> votes { get; set; }

        public List<string> group { get; set; }

        public string status { get; set; }





        

    }
}
