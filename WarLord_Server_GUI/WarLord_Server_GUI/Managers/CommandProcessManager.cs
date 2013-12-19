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

        protected override void cmdF_ReadyCancel()
        {
            MessageBox.Show("readyCancel");
        }

        protected override void cmdF_ReadyFind()
        {
            MessageBox.Show("readyFind");
        }


        public void CommandProcess(List<ArraySegment<byte>> cmd)
        {
            StringBuilder command = new StringBuilder();
            foreach (var item in cmd)
            {
                foreach (var t in item)
                {
                    command.Append(t.ToString());
                }
            }
            Delegate dg;
            CommandDic.TryGetValue(command.ToString(),out dg);
            dg.DynamicInvoke();
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
