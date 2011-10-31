using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MC.Mvc.Helpers
{
    public class WebClientHelper
    {
        public static WebClient Client
        {
            get
            {
                WebClient wc = new WebClient();
                Uri address = WebProxy.GetDefaultProxy().Address;
                if (address != null)
                {
                    //WebProxy proxy = new WebProxy(new System.Uri("http://gln-route.gillion.com.cn:8080"), true);
                    WebProxy proxy = new WebProxy(address, true);
                    proxy.Credentials = new NetworkCredential("zhangs", "chong2882@qq.com", "GILLION");
                    wc.Proxy = proxy;
                }
                return wc;
            }
        }
    }
}
