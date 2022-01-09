using System;
using System.Collections.Generic;
using System.Text;

namespace Nanny.Configuration
{
    public class StartOptions
    {

        public readonly static StartOptions TryForever = new StartOptions();
        public readonly static StartOptions TryOnce = new StartOptions();

    }
}
