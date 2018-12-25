using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Runtime.InteropServices;
using Dicom;
namespace XPChrome
{
    //1.定义代理信息的结构体
    public struct Struct_INTERNET_PROXY_INFO
    {
        public int dwAccessType;
        public IntPtr proxy;
        public IntPtr proxyBypass;
    };

    public partial class Form1 : Form
    {
        ChromiumWebBrowser m_chromeBrowser = null;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        //strProxy为代理IP:端口
        private void InternetSetOption(string strProxy)
        {

            //设置代理选项

            const int INTERNET_OPTION_PROXY = 38;

            //设置代理类型
            const int INTERNET_OPEN_TYPE_PROXY = 3;

            //设置代理类型，直接访问，不需要通过代理服务器了

            const int INTERNET_OPEN_TYPE_DIRECT = 1;


            Struct_INTERNET_PROXY_INFO struct_IPI;
            // Filling in structure 
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;

            //把代理地址设置到非托管内存地址中 
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);

            //代理通过本地连接到代理服务器上
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");

            // Allocating memory 

            //关联到内存
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
            }

            // Converting structure to IntPtr 

            //把结构体转换到句柄
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
        } 


        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_chromeBrowser = new ChromiumWebBrowser("www.baidu.com");
            m_chromeBrowser.AddressChanged += m_chromeBrowser_AddressChanged;
            this.splitContainer1.Panel2.Controls.Add(m_chromeBrowser);
        }

        void m_chromeBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                this.textBox1.Text = m_chromeBrowser.Address;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = this.textBox1.Text.Trim();
            url = !url.StartsWith("http") ? "http://" + url : url;
            this.textBox1.Text = url;
            m_chromeBrowser.Load("http://localhost:19955/test/?requesturl=" + url);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,e);
            }
        }
    }
}                                                                                                   
