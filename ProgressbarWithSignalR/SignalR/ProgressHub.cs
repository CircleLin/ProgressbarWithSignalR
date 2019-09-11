using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProgressbarWithSignalR.SignalR
{
    public class ProgressHub : Hub
    {
        private Timer timer = null;

        /// <summary>
        /// Client端斷線將觸發OnDisconnected function
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            //Dispose Timer
            if (stopCalled)
            {
                timer.Dispose();
            }
            return base.OnDisconnected(stopCalled);
        }

        public void CurrentProgress()
        {
            //每秒取得目前進度
            timer = new Timer(GetProcess, null, 0, 10);
        }

        //取得目前進度
        private void GetProcess(object state)
        {
            HttpContextBase httpcontext = Context.Request.GetHttpContext();
            try
            {
                //還沒有總筆數這個cache就先return
                if (httpcontext.Cache.Get("rowCount") == null)
                {
                    return;
                }

                double allcount = Convert.ToDouble(httpcontext.Cache.Get("rowCount"));

                if (httpcontext.Cache.Get("insertedRowNum") != null)
                {
                    //目前進行到的筆數
                    double insertedNum = Convert.ToDouble(httpcontext.Cache.Get("insertedRowNum"));

                    //計算目前進度的百分比
                    var percentage = ((double)insertedNum / allcount) * 100;
                    percentage = Math.Round(percentage, MidpointRounding.AwayFromZero);

                    //推播到client端
                    Clients.Caller.addprogress(percentage);
                }
            }
            catch
            {
                return;
            }
        }
    }
}