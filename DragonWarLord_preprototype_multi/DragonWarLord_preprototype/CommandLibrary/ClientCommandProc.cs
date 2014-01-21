using DragonWarLord_preprototype.GameLogic_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.GameLogic_B;

namespace DragonWarLord_preprototype.CommandLibrary
{
    class ClientCommandProc : ClientCommandLibrary
    {

        #region 공통 부분

        /// <summary>
        /// 니 차례다
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_YourTurn(string command)
        {
            TurnManager.Turn = true;
            MainForm.mainForm.EnabledButton(true);
            GamePlayManager.Instance.EndOfTurn();
        }
        /// <summary>
        /// 상대 차례다
        /// </summary>
        /// <param name="command"></param>
        protected override void cmdF_OpponentTurn(string command)
        {
            TurnManager.Turn = false;
            MainForm.mainForm.EnabledButton(false);
            GamePlayManager.Instance.EndOfTurn();
        }

        /// <summary>
        /// 노티
        /// </summary>
        /// <param name="command"></param>
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
                GamePlayManager.Instance.makeMainPlayer(data[1].Split(','), GameBoard.MY_PLAYERZONE);
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
                GamePlayManager.Instance.makeMainPlayer(data[1].Split(','), GameBoard.OPPONENT_PLAYERZONE);
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
                GamePlayManager.Instance.inputCard(data[1].Split(','), GameBoard.MY_CARDDECK);
                NetworkManager.ws.Send("YourCardDeck_OK;"); //응답
            }
            catch (Exception e)
            {
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
                GamePlayManager.Instance.inputCard(data[1].Split(','), GameBoard.OPPONENT_CARDDECK);
                NetworkManager.ws.Send("OpponentCardDeck_OK;"); //응답
            }
            catch (Exception e)
            {
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

        protected override void cmdF_FirstDistribute(string command)
        {
            GamePlayManager.Instance.firstDistribute();
        }

        protected override void cmdF_Distribute(string command)
        {
            GamePlayManager.Instance.Distribute();
        }

        protected override void cmdF_MakeResource(string command)
        {
            string[] position = command.Split(';');
            Card_Control card = GamePlayManager.Instance.findCard(position[1], position[2]);
            GamePlayManager.Instance.makeMana(card);    //마나생성
            GamePlayManager.Instance.CMR = false;
        }

        protected override void cmdF_MatchCard(string command)
        {
            string[] position = command.Split(';');
            Card_Control selectCard = GamePlayManager.Instance.findCard(position[1], position[2]);
            Card_Control targetCard = GamePlayManager.Instance.findCard(position[3], position[4]);
            if (selectCard != null && targetCard != null)
            {
                GamePlayManager.Instance.MatchCard(selectCard, targetCard);
            }
        }

        protected override void cmdF_UseCard(string command)
        {
            string[] position = command.Split(';');
            Card_Control card = GamePlayManager.Instance.findCard(position[1], position[2]);

            if (card.card.Skill.Equals("20"))
            {
                if (GamePlayManager.Instance.canPopCard(card, "20"))
                {
                    GameSkillManager.Instance.skill_card = card;    //자신 등록
                    GamePlayManager.Instance.gameSkill("20", card);
                }
                else
                {
                    //MessageBox.Show("마나가 부족합니다.");
                }
            }
            else
            {
                if (GamePlayManager.Instance.canPopCard(card))
                {

                    if (card.card.Skill.Equals("1"))
                    {
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("2"))
                    {
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("3"))
                    {
                        GamePlayManager.Instance.gameSkill("3", card);
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("4"))
                    {
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("5"))
                    {
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("7"))
                    {
                        GamePlayManager.Instance.gameSkill("7", card);
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("8"))
                    {
                        GamePlayManager.Instance.gameSkill("8", card);
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("9"))
                    {
                        GamePlayManager.Instance.gameSkill("9", card);
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("11"))
                    {
                        GamePlayManager.Instance.gameSkill("11", card);
                        GamePlayManager.Instance.popCard(card);
                    }
                    else if (card.card.Skill.Equals("16"))
                    {
                        GameSkillManager.Instance.skill_card = card;    //자신 등록
                        GamePlayManager.Instance.gameSkill("16", card);
                    }
                    else if (card.card.Skill.Equals("17"))
                    {
                        GameSkillManager.Instance.skill_card = card;    //자신 등록
                        GamePlayManager.Instance.gameSkill("17", card);
                    }
                    else if (card.card.Skill.Equals("18"))
                    {
                        GameSkillManager.Instance.skill_card = card;    //자신 등록
                        GamePlayManager.Instance.gameSkill("18", card);
                    }
                    else if (card.card.Skill.Equals("19"))
                    {
                        GameSkillManager.Instance.skill_card = card;    //자신 등록
                        GamePlayManager.Instance.gameSkill("19", card);
                    }
                    else if (card.card.Skill.Equals("21"))
                    {
                        GameSkillManager.Instance.skill_card = card;    //자신 등록
                        GamePlayManager.Instance.gameSkill("21", card);
                    }
                    else
                    {
                        GamePlayManager.Instance.popCard(card);
                    }
                }
                else
                {
                    //MessageBox.Show("마나가 부족합니다.");
                }
            }
            //=====카드 선택 초기화(null)전달
            GamePlayManager.Instance.CardSelectProc(null);
        }
    }
}
