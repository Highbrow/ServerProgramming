using Alchemy.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarLord_Server_GUI.Managers
{
    class CommandProcessManager : CommandLibrary
    {

        protected override void cmdF_ReadyCancel(ref UserContext aContext)
        {
            //MessageBox.Show("readyCancel");
            aContext.Send("readyCancel");
        }

        protected override void cmdF_ReadyFind(ref UserContext aContext)
        {
            //MessageBox.Show("readyFind");
            aContext.Send("readyFind");
        }


        public void CommandProcess(ref UserContext aContext)
        {
            StringBuilder command = new StringBuilder();
            foreach (var item in aContext.DataFrame.AsRaw())
            {
                foreach (var t in item)
                {
                    command.Append(t.ToString());
                }
            }
            Delegate dg;
            CommandDic.TryGetValue(command.ToString(),out dg);
            dg.DynamicInvoke(aContext);
        }

        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static CommandProcessManager CommandProcessManagerInstance = null;
        static readonly object padlock = new object();

        public static CommandProcessManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (CommandProcessManagerInstance == null)
                    {
                        CommandProcessManagerInstance = new CommandProcessManager();
                    }
                    return CommandProcessManagerInstance;
                }
            }
        }

    }
}
