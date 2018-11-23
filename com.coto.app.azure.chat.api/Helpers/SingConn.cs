using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.coto.app.azure.chat.api.Helpers
{
    public class SingConn
    {
        private static SingConn instance;

        private SingConn() { }

        public static SingConn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingConn();
                }
                return instance;
            }
        }

        internal void StartTime()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 5000;
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(HubContextHelper.VerifyMessages);
            aTimer.Enabled = true;
        }
    }
}