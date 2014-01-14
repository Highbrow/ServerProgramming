using DragonWarLord_preprototype.CardLibrary;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace DragonWarLord_preprototype
{
    class DataBaseManager
    {
        //=====[카드 불러오기]=====
        private static string strConn = "Server=14.63.170.220;Database=dragonwarlord;Uid=haeggong2;Pwd=Rhdtm11;";
        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        private static string query = "select * from user where _id = '1'";

        public static void inputCard(string[] card_list, bool my)
        {
            conn = new MySqlConnection(strConn);
            
            if (my)
            {
                foreach (string q in card_list)
                {
                    conn.Open();
                    query = "select * from card where _id = '" + q + "'";
                    cmd = new MySqlCommand(query);

                    cmd.Connection = conn;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            GameBoard.My_CardDeck.Add(new Card_Control()
                            {
                                card = new Card()
                                {
                                    Name = reader.GetString(1),
                                    Attribute = reader.GetString(2),
                                    Type = reader.GetString(3),
                                    Class = reader.GetString(4),
                                    Species = (reader.IsDBNull(5)) ? "" : reader.GetString(5),
                                    Consumption = reader.GetString(6),
                                    Ap = reader.GetInt16(7),
                                    Hp = reader.GetInt16(8),
                                    Rp = reader.GetInt16(9),
                                    Limited_amount = reader.GetInt16(10),
                                    Skill = reader.GetString(11),
                                    Information = (reader.IsDBNull(12)) ? "" : reader.GetString(12),
                                    Image_file = reader.GetString(13),
                                    thisTurnAP = reader.GetInt16(7),
                                    thisTurnHP = reader.GetInt16(8),
                                }
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message.ToString());
                    }
                    conn.Close();
                }
            }
            else
            {

                foreach (string q in card_list)
                {
                    conn.Open();
                    query = "select * from card where _id = '" + q + "'";
                    cmd = new MySqlCommand(query);

                    cmd.Connection = conn;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            GameBoard.Opponent_CardDeck.Add(new Card_Control()
                            {
                                card = new Card()
                                {
                                    Name = reader.GetString(1),
                                    Attribute = reader.GetString(2),
                                    Type = reader.GetString(3),
                                    Class = reader.GetString(4),
                                    Species = (reader.IsDBNull(5)) ? "" : reader.GetString(5),
                                    Consumption = reader.GetString(6),
                                    Ap = reader.GetInt16(7),
                                    Hp = reader.GetInt16(8),
                                    Rp = reader.GetInt16(9),
                                    Limited_amount = reader.GetInt16(10),
                                    Skill = reader.GetString(11),
                                    Information = (reader.IsDBNull(12)) ? "" : reader.GetString(12),
                                    Image_file = reader.GetString(13),
                                    thisTurnAP = reader.GetInt16(7),
                                    thisTurnHP = reader.GetInt16(8),
                                }
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message.ToString());
                    }
                    conn.Close();
                }
            }
        }


        //=====[ 플레이어 생성 ]=====
        public static Card_Control makeMainPlayer(string userId)
        {
            Card_Control userCharacter = null;
            conn = new MySqlConnection(strConn);
            conn.Open();
            query = "select * from user where _id = '" + userId + "'";
            cmd = new MySqlCommand(query);

            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    userCharacter = new Card_Control()
                    {
                        card = new Card()
                        {
                            Name = reader.GetString(1),
                            Attribute = reader.GetString(2),
                            Type = reader.GetString(3),
                            Class = reader.GetString(4),
                            Species = (reader.IsDBNull(5)) ? "" : reader.GetString(5),
                            Consumption = reader.GetString(6),
                            Ap = reader.GetInt16(7),
                            Hp = reader.GetInt16(8),
                            Rp = reader.GetInt16(9),
                            Limited_amount = reader.GetInt16(10),
                            Skill = reader.GetString(11),
                            Information = (reader.IsDBNull(12)) ? "" : reader.GetString(12),
                            Image_file = reader.GetString(13),
                            thisTurnAP = reader.GetInt16(7),
                            thisTurnHP = reader.GetInt16(8),
                        }
                    };
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            conn.Close();

            return userCharacter;
        }
    }


}
