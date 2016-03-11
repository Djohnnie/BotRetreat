using System.Collections.Generic;

namespace BotRetreat.DataTransferObjects
{
    public class Game
    {
        public Arena Arena { get; set; }

        public List<Bot> Bots { get; set; }

        public List<History> History { get; set; }
    }
}