using CefSharp;
using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace XPChrome
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            var settings = new CefSettings();
            settings.RemoteDebuggingPort = 8088;
            settings.CefCommandLineArgs.Add("enable-media-stream", "enable-media-stream");
            settings.IgnoreCertificateErrors = true;
            settings.LogSeverity = LogSeverity.Verbose;


            Cef.Initialize(settings);

            try
            {
                var url = new Url("http://localhost:19955");
                var hostConfig = new HostConfiguration();
                hostConfig.UrlReservations = new UrlReservations { CreateAutomatically = true };
                var host = new NancyHost(hostConfig, url);
                host.Start();
                //int port = 9001;
                //string url = string.Format("http://localhost:{0}/", port);
                //var _host = new NancyHost(new Uri(url));
                //_host.Start();
                //Process.Start(url);
                //MessageBox.Show("站点启动成功,请打开{0}进行浏览", url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("站点启动失败：" + ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
