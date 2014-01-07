using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleClient
{
    class eventManager
    {
        public delegate void dchangeStatus(object obj);
        public event dchangeStatus cardDestruction;
        public event dchangeStatus cardBattle;


        public eventManager()
        {
            cardDestruction += new dchangeStatus(changeStatus);
            cardBattle += new dchangeStatus(changeStatus);
        }

        public void changeStatus(object obj)
        {
            string message = (string)obj;
        }
    }
}
