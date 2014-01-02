using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.GameLogic_B;

namespace TestConsoleClient.GameLogic_B
{
    class GameSkillManager
    {
        public int skill_selectTarget = 0;


        public ConcurrentDictionary<string, Delegate> SkillDictionary = new ConcurrentDictionary<string, Delegate>();
        public delegate void skillDelegate(Card_Control card_con);

        protected void skill_1_vamfire(Card_Control card_con)
        {
            MessageBox.Show("뱀파이어의 힘을 보여주마!");
            skill_selectTarget = 21;
            //zoneForm form = new zoneForm();
            //form.ShowDialog();
        }

        protected void skill_21_succubus(Card_Control card_con)
        {
        }

        public GameSkillManager()
        {
            SkillDictionary.TryAdd("1", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("2", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("3", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("4", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("5", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("6", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("7", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("8", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("9", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("10", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("11", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("12", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("13", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("14", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("15", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("16", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("17", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("18", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("19", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("20", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("21", new skillDelegate(skill_21_succubus));
        }


        /**********************************************
          ***********[[ Singleton 적용 ]]****************
          ***********************************************/
        static GameSkillManager GameSkillManagerInstance = null;
        static readonly object padlock = new object();

        public static GameSkillManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (GameSkillManagerInstance == null)
                    {
                        GameSkillManagerInstance = new GameSkillManager();
                    }
                    return GameSkillManagerInstance;
                }
            }
        }
    }
}
