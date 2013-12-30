using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace TestConsoleClient
{
    class Program
    {

        WebSocket ws;

        Program()
        {
            ws = new WebSocket("ws://127.0.0.1:9001");
            ws.OnOpen += (object sender, System.EventArgs e) =>
            {
                Console.WriteLine("Open");
            };
            ws.Connect();

            ws.OnMessage += (sender, e) =>
            {
                if (e.Data == "1")
                {
                    Console.WriteLine("" + e.Data);
                    Console.WriteLine("join success.");                   
                }
                
            };
        }
       
    }
}
