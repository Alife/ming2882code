using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MC.WcfLib.Sample
{
    /// <summary>
    /// IHello接口
    /// </summary>
    [ServiceContract]
    public interface IHello
    {
        [OperationContract]
        [WebGet(UriTemplate = "sample/{name}", ResponseFormat = WebMessageFormat.Json)]
        string SayHello(string name);
    }
}
