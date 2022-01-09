using System;
using System.Collections.Generic;
using System.Text;

namespace Nanny.Configuration
{
    public class StartOptions
    {
        private readonly string _reconnectionType;

        public readonly static StartOptions TryForever = new StartOptions("forever");
        public readonly static StartOptions TryOnce = new StartOptions("once");

        protected StartOptions(string reconnectionType)
        {
            _reconnectionType = reconnectionType;
        }
    }
}
