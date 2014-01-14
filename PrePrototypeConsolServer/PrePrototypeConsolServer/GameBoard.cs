using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class GameBoard
    {
        public const int PLAYER1_CARDDECK = 1;
        public const int PLAYER2_CARDDECK = 2;
        public const int PLAYER1_HANDSZONE = 3;
        public const int PLAYER2_HANDSZONE = 4;
        public const int PLAYER1_WARZONE = 5;
        public const int PLAYER2_WARZONE = 6;
        public const int PLAYER1_MANAZONE = 7;
        public const int PLAYER2_MANAZONE = 8;
        public const int PLAYER1_TOMBZONE = 9;
        public const int PLAYER2_TOMBZONE = 10;
        public const int PLAYER1_PLAYERZONE = 100;
        public const int PLAYER2_PLAYERZONE = 200;

        //=====[ 카드 덱 ]=====
        public List<Card> P1_CardDeck = new List<Card>();
        public List<Card> P2_CardDeck = new List<Card>();
        //=====[플레이어]=====
        public Card P1_PlayerZone = new Card();
        public Card P2_PlayerZone = new Card();
        //=====[ 플레이어 핸드 존]=====
        public List<Card> P1_HandsZone = new List<Card>();
        public List<Card> P2_HandsZone = new List<Card>();
        //=====[ 전장 ]=====
        public List<Card> P1_WarZone = new List<Card>();
        public List<Card> P2_WarZone = new List<Card>();
        //=====[ 마나 존 ]=====
        public List<Card> P1_ManaZone = new List<Card>();
        public List<Card> P2_ManaZone = new List<Card>();
        //=====[ 무덤 존 ]=====
        public List<Card> P1_TombZone = new List<Card>();
        public List<Card> P2_TombZone = new List<Card>();
    }
}
