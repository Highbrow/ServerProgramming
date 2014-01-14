using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class TurnManager
    {
        public Alchemy.Classes.UserContext Player1 { get; set; }

        public Alchemy.Classes.UserContext Player2 { get; set; }

        public Alchemy.Classes.UserContext Turn { get; set; }
    }
}
