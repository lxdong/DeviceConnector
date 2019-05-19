using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DeviceConnector.Service
{
    public partial class DeviceConnector : ServiceBase
    {
        #region 变量
        static HttpListener httpListener;
        #endregion
        public DeviceConnector()
        {
            InitializeComponent();
            InitIoC();
        }

        private void InitIoC()
        {
            IUnityContainer container = new UnityContainer();

            
        }

        protected override void OnStart(string[] args)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("");
            httpListener.Start();
            httpListener.BeginGetContext(Result, null);
        }

        private void Result(IAsyncResult ar)
        {
            httpListener.BeginGetContext(Result, null);
            var guid = Guid.NewGuid().ToString();
            var context = httpListener.EndGetContext(ar);
            var request = context.Request;
            var response = context.Response;
            context.Response.ContentType = "";
            context.Response.AddHeader("", "");

        }

        protected override void OnStop()
        {
        }


    }
}
