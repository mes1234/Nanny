using System;
using System.Collections.Generic;
using System.Text;

namespace Nanny.Configuration
{
    public class StartOptions
    {
        private readonly string _reconnectionType = "once";

        public static StartOptions TryForever = new StartOptions("forever");
        public static StartOptions TryOnce = new StartOptions("once");

        protected StartOptions(string reconnectionType)
        {
            _reconnectionType = reconnectionType;
        }
    }
}
