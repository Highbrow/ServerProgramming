using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameBoard
    {
        //=====[ 카드 덱 ]=====
        //public Queue<Card> P1_CardDeck = new Queue<Card>();
        //public Queue<Card> P2_CardDeck = new Queue<Card>();
        public List<Card> P1_CardDeck = new List<Card>();
        public List<Card> P2_CardDeck = new List<Card>();
        //=====[ 플레이어 핸드 존]=====
        public List<Card> P1_HandsZone = new List<Card>();
        public List<Card> P2_HandsZone = new List<Card>();
        //=====[ 전장 ]=====
        public List<Card> P1_WarZone = new List<Card>();
        public List<Card> P2_WarZone = new List<Card>();
        //=====[ 마나 존 ]=====
        public List<Card> p1_ManaZone;
        public List<Card> p2_ManaZone;
        //=====[ 무덤 존 ]=====
        public List<Card> p1_TombZone;
        public List<Card> p2_TombZone;
        
    }
}
