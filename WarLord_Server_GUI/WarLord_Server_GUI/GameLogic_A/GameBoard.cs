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
        public List<Card> p1_CardDeck;
        public List<Card> p2_CardDeck;
        //=====[ 플레이어 핸드 존]=====
        public List<Card> p1_HandsZone;
        public List<Card> p2_HandsZone;
        //=====[ 전장 ]=====
        public List<Card> P1_WarZone;
        public List<Card> P2_WarZone;
        //=====[ 마나 존 ]=====
        public List<Card> p1_ManaZone;
        public List<Card> p2_ManaZone;
        //=====[ 무덤 존 ]=====
        public List<Card> p1_TombZone;
        public List<Card> p2_TombZone;
        
    }
}
