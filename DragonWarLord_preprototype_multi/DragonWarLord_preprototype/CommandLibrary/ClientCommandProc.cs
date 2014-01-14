using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonWarLord_preprototype.CommandLibrary
{
    class ClientCommandProc :ClientCommandLibrary
    {

        protected override void cmdF_GameWait(string command)
        {
            StartForm.startForm.setText_lb_status("waiting for the other party..");
        }

        protected override void cmdF_CreatedRoom(string command)
        {
            StartForm.startForm.DialogResult = DialogResult.OK;
        }

        protected override void cmdF_YourCharacter(string command)
        {
            try
            {
                string[] data = command.Split(';');
                MessageBox.Show(data[1]);   //DEBUG
                CardDealer.Instance.makeMainPlayer(data[1].Split(','), CardDealer.PLAYER1_PLAYERZONE);
                NetworkManager.ws.Send("YourCharacter_OK;"); //응답
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Data.ToString());
            }
        }

        protected override void cmdF_OpponentCharacter(string command)
        {
            try
            {
                string[] data = command.Split(';');
                MessageBox.Show(data[1]);   //DEBUG
                CardDealer.Instance.makeMainPlayer(data[1].Split(','), CardDealer.PLAYER2_PLAYERZONE);
                NetworkManager.ws.Send("OpponentCharacter_OK;"); //응답
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Data.ToString());
            }
        }


        protected override void cmdF_YourCardDeck(string command)
        {
            try
            {
                string[] data = command.Split(';');
                MessageBox.Show(data[1]);   //DEBUG
                CardDealer.Instance.inputCard(data[1].Split(','), CardDealer.PLAYER1_CARDDECK);
                NetworkManager.ws.Send("YourCardDeck_OK;"); //응답
            }catch(Exception e){
                MessageBox.Show(e.Data.ToString());
            }
        }

        protected override void cmdF_OpponentCardDeck(string command)
        {
            try
            {
                string[] data = command.Split(';');
                MessageBox.Show(data[1]);   //DEBUG
                CardDealer.Instance.inputCard(data[1].Split(','), CardDealer.PLAYER2_CARDDECK);
                NetworkManager.ws.Send("OpponentCardDeck_OK;"); //응답
            }catch(Exception e){
                MessageBox.Show(e.Data.ToString());
            }
        }

        protected override void cmdF_StartGame(string command)
        {
            MessageBox.Show("게임을 시작합니다.");
            NetworkManager.ws.Send("StartGame_OK");
        }

        protected override void cmdF_Message(string command)
        {
            string[] data = command.Split(';');
            MessageBox.Show(data[1]);   //DEBUG
        }
    }
}
