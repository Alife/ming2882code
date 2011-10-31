using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MC.WcfLib.Sample
{
    public class Hello: IHello
    {
        public string SayHello(string name)
        {
            return "Hello: " + name;
        }
    }
}
