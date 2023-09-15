using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH.Classes
{
    internal class SessaoAtual
    {
        private static volatile SessaoAtual instance;
        private static object sync = new Object();

        private SessaoAtual() { }

        public static SessaoAtual Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new SessaoAtual();
                        }
                    }
                }
                return instance;
            }

        }

        public int UserID { get; set; }


    }
}
