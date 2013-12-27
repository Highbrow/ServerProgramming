using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLord_Server_GUI.GameLogic_A
{
    class Dealer : Character
    {
        private Player _p1;
        private Player _p2;
        private GameBoard _board;

        public Dealer(ref Player p1, ref Player p2, ref GameBoard board)
        {
            this._p1 = p1;
            this._p2 = p2;
            callCardDeck(); // 카드 정보 호출
            this._board = board;

            shuffle<Card>(board.p1_CardDeck);   //p1 카드덱 섞기
            shuffle<Card>(board.p2_CardDeck);   //p2 카드덱 섞기
        }

        //=====[ 카드 불러오기 ]=====
        private List<Card> callCardDeck()
        {
            List<Card> cardDeck= null;

            return cardDeck;
        }

        //=====[ 카드 섞기 ]=====
        private static Random _rnd = new Random();

        public void shuffle<T>(List<T> cardDeckShuffle, int numberOfTimesToShuffle = 5)
        {
            List<T> newList = new List<T>();
            for (int i = 0; i < numberOfTimesToShuffle; i++)
            {
                while (cardDeckShuffle.Count > 0)
                {
                    int index = _rnd.Next(cardDeckShuffle.Count);
                    newList.Add(cardDeckShuffle[index]);
                    cardDeckShuffle.RemoveAt(index);
                }
                cardDeckShuffle.AddRange(newList);
                newList.Clear();
            }
        }

    }
}
