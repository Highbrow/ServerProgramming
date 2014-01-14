using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class GameSkillManager
    {

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
