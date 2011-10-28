using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CometController : AsyncController
    {
        //LongPolling Action 1 - 处理客户端发起的请求
        public void LongPollingAsync()
        {
            //计时器，5秒种触发一次Elapsed事件
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            //告诉ASP.NET接下来将进行一个异步操作
            AsyncManager.OutstandingOperations.Increment();
            //订阅计时器的Elapsed事件
            timer.Elapsed += (sender, e) =>
            {
                //保存将要传递给LongPollingCompleted的参数
                AsyncManager.Parameters["now"] = e.SignalTime;
                //告诉ASP.NET异步操作已完成，进行LongPollingCompleted方法的调用
                AsyncManager.OutstandingOperations.Decrement();
            };
            //启动计时器
            timer.Start();
        }

        //LongPolling Action 2 - 异步处理完成，向客户端发送响应
        public ActionResult LongPollingCompleted(DateTime now)
        {
            return Json(new
            {
                d = now.ToString("MM-dd HH:mm:ss ") +
                    "-- Welcome to who are you!"
            },
                JsonRequestBehavior.AllowGet);
        }
    }
}
