using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonWarLord_preprototype
{

    class TurnManager
    {
        public static bool Turn { get; set; }

        #region 싱글톤
        static TurnManager TurnManagerInstance = null;
        static readonly object padlock = new object();
        /// <summary>
        /// Singleton 적용
        /// </summary>
        public static TurnManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (TurnManagerInstance == null)
                    {
                        TurnManagerInstance = new TurnManager();
                    }
                    return TurnManagerInstance;
                }
            }
        }

        #endregion
    }
}
