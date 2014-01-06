using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleClient.CardLibrary
{
    class Card
    {
        public string Name { get; set; }
        public string Attribute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public string Species { get; set; }
        public string Consumption { get; set; }
        public int Ap { get; set; }
        public int Hp { get; set; }
        public int Rp { get; set; }
        public int Limited_amount { get; set; }
        public string Skill { get; set; }
        public string Information { get; set; }
        public int thisTurnHP { get; set; }
        public int thisTurnAP { get; set; }

        public void Attack(ref Card target_Card)
        {

        }
        public void UnderAttack()
        {

        }

    }
}
