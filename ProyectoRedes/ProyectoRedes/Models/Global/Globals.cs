namespace ProyectoRedes.Models.Global
{
    public class Globals
{
        public static string gameId { get; set; }
        public static string gameName { get; set; }    

        public static string playerName { get; set; }

        public static string password { get; set; }

        public static List<string> players { get; set; }
        
        public static List<string> enemies { get; set; }

        //utilities for rounds
        public static string roundId { get; set; }
        public static string leader { get; set; }

        public static string status { get; set; }

        public static string result { get; set; }

        public static string phase { get; set; }
        public static List<string> group { get; set;}

        public static List<bool> votes { get; set; }

    }
}
