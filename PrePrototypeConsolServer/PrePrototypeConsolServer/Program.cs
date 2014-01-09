using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class Program
    {
        ServerManager SM;

        Program()
        {
            SM = ServerManager.Instance;
            var command = string.Empty;
            while (command != "exit")
            {
                command = Console.ReadLine();
            } 
            SM._wServer.Stop();
        }

        static void Main(string[] args)
        {
            new Program();
        }

    }
}
