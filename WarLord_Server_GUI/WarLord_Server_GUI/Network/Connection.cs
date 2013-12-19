﻿using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLord_Server_GUI.Network
{
    /**********************************************
    ***************[[ Connection ]]****************
    ***********************************************/
    public class Connection
    {
        //public System.Threading.Timer timer;
        public UserContext Context { get; set; }
        public Connection()
        {
            //this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state)
        {
            try
            {
                Context.Send("[" + Context.ClientAddress.ToString() + "] " + System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}