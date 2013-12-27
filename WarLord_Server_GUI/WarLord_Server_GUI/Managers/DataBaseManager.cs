using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace WarLord_Server_GUI.Managers
{
    class DataBaseManager
    {
        private string strConn = "Server=localhost;Database=dragonwarlord;Uid=root;Pwd=dnjfhem2013$;";
        public MySqlConnection conn;
        public MySqlCommand cmd;
        private string query = "select * from user where _id = '1'";

        public DataBaseManager()
        {
            conn = new MySqlConnection(strConn);
            conn.Open();
            createCard(readCard());
            conn.Close();
        }

        public string[] readCard()
        {
            string[] result = null;
            cmd = new MySqlCommand(query);
            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    result = reader.GetString(1).Split(';');
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            return result;
        }



        List<Card> cardDeck = new List<Card>();

        public void createCard(string[] deck)
        {
            foreach (string d in deck)
            {
                query = "select * from card where _id = '" + d + "'";
                cmd = new MySqlCommand(query);
                MessageBox.Show("asd");

                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
               

                try
                {
                    while (reader.Read())
                    {
                        
                        cardDeck.Add(new Card()
                        {
                            Name = reader.GetString(1),
                            Attribute = reader.GetString(2),
                            Type = reader.GetString(3),
                            Class = reader.GetString(4),
                            Species = reader.GetString(5),
                            Consumption = reader.GetString(6),
                            Ap = reader.GetInt16(7),
                            Hp = reader.GetInt16(8),
                            Rp = reader.GetInt16(9),
                            Limited_amount = reader.GetInt16(10),
                            Skill = reader.GetString(11),
                            Information = reader.GetString(12),
                        });
                        
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }

                MessageBox.Show(cardDeck.Count+"");
            }

        }

    }
}
