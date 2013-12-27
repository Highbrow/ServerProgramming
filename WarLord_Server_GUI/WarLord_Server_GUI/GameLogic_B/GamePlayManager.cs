using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.GameLogic_A;

namespace WarLord_Server_GUI.GameLogic_B
{
    class GamePlayManager
    {

        //=====[ Match ]=====
        public int[] MatchCard(Card selectCard, Card tagetCard)
        {
            int[] result = new int[2];
            result[0] = selectCard.Hp -= tagetCard.Ap;  // 본인 생명력
            result[1] = tagetCard.Hp -= selectCard.Ap;  // 대상 생명력

            return result;  // 0 : 본인 생명력, 1: 대상 생명력
        }


    }
}
