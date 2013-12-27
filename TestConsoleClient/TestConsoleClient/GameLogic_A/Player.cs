using Alchemy.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLord_Server_GUI.GameLogic_A
{
    class Player : Character
    {
        UserContext aContext = null;
        bool player_Active = false;


        public Player(UserContext context)
        {
            this.aContext = context;
            this.player_Active = true;

            new Card()
            {
                Ap = 10,
                Hp = 5
            };

        }

    }
}
