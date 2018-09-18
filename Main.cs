using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace startClient
{
    class Main
    {
        public static int port = 409;// 端口
        public static string host = "127.0.0.1";// 服务器IP
        public static int serverport = 721;// 服务器端口
        public static bool errorinfo = false;// 输出错误信息
        public static bool forwardinfo = false;// 转发信息
        public static bool off = true;//关闭
        public static Form1 form1 = null;

        public static int forwardfun(int i)
        {
            return ~i * -1;
        }

        public static int receivefun(int i)
        {
            return ~(i * -1);
        }

        public static void main()
        {
            try
            {
                TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                listener.Start();
                form1.outText("start...\r\n");
                while (off)
                {
                    Receive receive = new Receive(listener.AcceptTcpClient());
                    Thread thread = new Thread(new ThreadStart(receive.run)) { IsBackground = true };
                    thread.Start();
                };
                listener.Stop();
                off = true;
            }
            catch (Exception e) { if (Main.errorinfo) { Main.form1.outText(e.Message.ToString() + "\r\n"); } }
        }
    }
}
