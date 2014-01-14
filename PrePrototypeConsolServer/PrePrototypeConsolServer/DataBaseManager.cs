using Alchemy.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class DataBaseManager
    {

        private string strConn = "Server=14.63.170.220;Database=dragonwarlord;Uid=haeggong2;Pwd=Rhdtm11;";
        public MySqlConnection conn;
        public MySqlCommand cmd;
        private string query = null;
        private GameBoard gameBoard;

        List<string> _userList;

        public DataBaseManager(ref GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;

            this._userList = new List<string>();
            this._userList.Add("jang");
            this._userList.Add("taelimoh");

            createUser(_userList);
            createCard();
        }

        /// <summary>
        /// 유저 불러오기 (수정할 예정:임시로 string 처리)
        /// public List<User> createUser(List<UserContext> userList)
        /// </summary>
        /// <param name="userList"></param>
        public void createUser(List<string> userList)
        {
            conn = new MySqlConnection(strConn);

            conn.Open();

            query = "select * from user where _id = '" + userList[0] + "'";
            cmd = new MySqlCommand(query);

            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    gameBoard.P1_PlayerZone = new Card()
                    {
                        Id = reader.GetString(0),
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
                        position = 0,
                        thisTurnAP = reader.GetInt16(7),
                        thisTurnHP = reader.GetInt16(8),
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
            conn.Close();

            //\\\\\\\\\\\\\\\\\\\\\\\\\
            conn = new MySqlConnection(strConn);

            conn.Open();

            query = "select * from user where _id = '" + userList[1] + "'";
            cmd = new MySqlCommand(query);

            cmd.Connection = conn;
            MySqlDataReader reader2 = cmd.ExecuteReader();

            try
            {
                while (reader2.Read())
                {
                    gameBoard.P2_PlayerZone = new Card()
                    {
                        Id = reader2.GetString(0),
                        Name = reader2.GetString(1),
                        Attribute = reader2.GetString(2),
                        Type = reader2.GetString(3),
                        Class = reader2.GetString(4),
                        Species = (reader2.IsDBNull(5)) ? "" : reader2.GetString(5),
                        Consumption = reader2.GetString(6),
                        Ap = reader2.GetInt16(7),
                        Hp = reader2.GetInt16(8),
                        Rp = reader2.GetInt16(9),
                        Limited_amount = reader2.GetInt16(10),
                        Skill = reader2.GetString(11),
                        Information = (reader2.IsDBNull(12)) ? "" : reader2.GetString(12),
                        Image_file = reader2.GetString(13),
                        position = 0,
                        thisTurnAP = reader2.GetInt16(7),
                        thisTurnHP = reader2.GetInt16(8),
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
            conn.Close();
        }

        public void createCard()
        {
            conn = new MySqlConnection(strConn);
            conn.Open();

            query = "select * from card";
            cmd = new MySqlCommand(query);

            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.GetInt16(10); i++)
                    {
                        gameBoard.P1_CardDeck.Add(
                               new Card()
                                    {
                                        Id = reader.GetString(0),
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
                                        position = 0,
                                        thisTurnAP = reader.GetInt16(7),
                                        thisTurnHP = reader.GetInt16(8),
                                    }
                            );
                        gameBoard.P2_CardDeck.Add(new Card()
                            {
                                Id = reader.GetString(0),
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
                                position = 0,
                                thisTurnAP = reader.GetInt16(7),
                                thisTurnHP = reader.GetInt16(8),
                            });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
            conn.Close();
        }
    }
}
