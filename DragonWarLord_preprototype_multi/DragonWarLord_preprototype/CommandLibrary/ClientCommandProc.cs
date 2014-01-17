using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace DragonWarLord_preprototype.CommandLibrary
{
    class ClientCommandProc :ClientCommandLibrary
    {

        #region 공통 부분
        /// <summary>
        /// 카드 옮겨라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_MoveCard(string command)
        {
            try
            {               
                CardDealer.Instance.moveCard(command.Split(';'));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Data.ToString());
            }
        }

        /// <summary>
        /// 자원 정보 업데이트 시켜라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_Resource(string command)
        {
            string[] resource = command.Split(';');

            //=====[ 전체 자원 ]====
            GameBoard.Instance.My_Resource_dark = Convert.ToInt32(resource[1]);
            GameBoard.Instance.My_Resource_fire = Convert.ToInt32(resource[2]);

            MainForm.mf.setText_My_cnt_dark(GameBoard.Instance.My_Resource_dark.ToString());
            MainForm.mf.setText_My_cnt_fire(GameBoard.Instance.My_Resource_fire.ToString());

            //=====[ 남은 자원 ]====
            GameBoard.Instance.My_RemainResource_dark = Convert.ToInt32(resource[3]);
            GameBoard.Instance.My_RemainResource_fire = Convert.ToInt32(resource[4]);
            GameBoard.Instance.My_RemainResource_all = Convert.ToInt32(resource[5]);

            MainForm.mf.setText_My_remain_dark(GameBoard.Instance.My_RemainResource_dark.ToString());
            MainForm.mf.setText_My_remain_fire(GameBoard.Instance.My_RemainResource_fire.ToString());
            MainForm.mf.setText_My_remain_all(GameBoard.Instance.My_RemainResource_all.ToString());

            GameBoard.Instance.My_UsedResource = Convert.ToInt32(resource[6]);

            MainForm.mf.setText_My_use_all(GameBoard.Instance.My_UsedResource.ToString());

            //=====[ 전체 자원 ]====
            GameBoard.Instance.Opponent_Resource_dark = Convert.ToInt32(resource[7]);
            GameBoard.Instance.Opponent_Resource_fire = Convert.ToInt32(resource[8]);

            MainForm.mf.setText_Opponent_cnt_dark(GameBoard.Instance.Opponent_Resource_dark.ToString());
            MainForm.mf.setText_Opponent_cnt_fire(GameBoard.Instance.Opponent_Resource_fire.ToString());

            //=====[ 남은 자원 ]====
            GameBoard.Instance.Opponent_RemainResource_dark = Convert.ToInt32(resource[9]);
            GameBoard.Instance.Opponent_RemainResource_fire = Convert.ToInt32(resource[10]);
            GameBoard.Instance.Opponent_RemainResource_all = Convert.ToInt32(resource[11]);

            MainForm.mf.setText_Opponent_remain_dark(GameBoard.Instance.Opponent_RemainResource_dark.ToString());
            MainForm.mf.setText_Opponent_remain_fire(GameBoard.Instance.Opponent_RemainResource_fire.ToString());
            MainForm.mf.setText_Opponent_remain_all(GameBoard.Instance.Opponent_RemainResource_all.ToString());

            GameBoard.Instance.Opponent_UsedResource = Convert.ToInt32(resource[12]);

            MainForm.mf.setText_Opponent_use_all(GameBoard.Instance.Opponent_UsedResource.ToString());
        }

        /// <summary>
        /// 니 차례다
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_YourTurn(string command)
        {
            TurnManager.Turn = true;
            CardDealer.Instance.canMakeResource = true;
            MainForm.mf.EnabledButton(true);

            foreach(Card_Control card in GameBoard.My_HandsZone){
                card.activatable = true;
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
            }
            foreach (Card_Control card in GameBoard.My_WarZone)
            {
                card.activatable = true;
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
            }
            foreach (Card_Control card in GameBoard.Opponent_HandsZone)
            {
                card.activatable = true;
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
            }
            foreach (Card_Control card in GameBoard.Opponent_WarZone)
            {
                card.activatable = true;
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
            }

        }
        /// <summary>
        /// 상대 차례다
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_OpponentTurn(string command)
        {
            TurnManager.Turn = false;
            MainForm.mf.EnabledButton(false);
        }

        protected override void cmdF_Message(string command)
        {
            string[] data = command.Split(';');
            MessageBox.Show(data[1]);
        }
        #endregion 공통 부분

        #region 게임 준비 과정 부분
        /// <summary>
        /// 상대방 기다려라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_GameWait(string command)
        {
            StartForm.startForm.setText_lb_status("waiting for the other party..");
        }
        /// <summary>
        /// 방만들었다
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_CreatedRoom(string command)
        {
            StartForm.startForm.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 니 캐릭터 받아라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_YourCharacter(string command)
        {
            try
            {
                string[] data = command.Split(';');
                CardDealer.Instance.makeMainPlayer(data[1].Split(','), GameBoard.MY_PLAYERZONE);
                NetworkManager.ws.Send("YourCharacter_OK;"); //응답
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Data.ToString());
            }
        }
        /// <summary>
        /// 상대 캐릭터 받아라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_OpponentCharacter(string command)
        {
            try
            {
                string[] data = command.Split(';');
                CardDealer.Instance.makeMainPlayer(data[1].Split(','), GameBoard.OPPONENT_PLAYERZONE);
                NetworkManager.ws.Send("OpponentCharacter_OK;"); //응답
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Data.ToString());
            }
        }

        /// <summary>
        /// 니 카드 받아라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_YourCardDeck(string command)
        {
            try
            {
                string[] data = command.Split(';');
                CardDealer.Instance.inputCard(data[1].Split(','), GameBoard.MY_CARDDECK);
                NetworkManager.ws.Send("YourCardDeck_OK;"); //응답
            }catch(Exception e){
                MessageBox.Show(e.Data.ToString());
            }
        }
        /// <summary>
        /// 상대 카드 받아라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_OpponentCardDeck(string command)
        {
            try
            {
                string[] data = command.Split(';');
                CardDealer.Instance.inputCard(data[1].Split(','), GameBoard.OPPONENT_CARDDECK);
                NetworkManager.ws.Send("OpponentCardDeck_OK;"); //응답
            }catch(Exception e){
                MessageBox.Show(e.Data.ToString());
            }
        }
        /// <summary>
        /// 게임 시작해라
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_StartGame(string command)
        {
            MessageBox.Show("게임을 시작합니다.");
            NetworkManager.ws.Send("StartGame_OK");
        }
        #endregion 게임 준비 과정 부분
    }
}
