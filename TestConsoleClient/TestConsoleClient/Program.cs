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
            startClient();

            ws.OnMessage += (sender, e) =>
            {
                if (e.Data == "1")
                {
                    Console.WriteLine("" + e.Data);
                    Console.WriteLine("join success.");                   
                }
                
            };
        }

        void test2()
        {

        }


        bool test = true;

        void startClient()
        {
            switch (menu())
            {
                case "1":
                    Console.WriteLine("i'm Ready...");
                    break;
                case "2":
                    break;
                case "3":
                    break;

            }
        }


        string menu()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("|  < Client >                |");
            Console.WriteLine("| 1. readyGame               |");
            Console.WriteLine("| 2. Cancel Ready Game       |");
            Console.WriteLine("| 3. exit                    |");
            Console.WriteLine("==============================");
            Console.WriteLine("=>>  ");

            return Console.ReadLine();
        }

       
    }
}
