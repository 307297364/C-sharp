using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace startClient
{
    class Receive
    {
        private TcpClient thissocket;

        public Receive(object tcpClient)
        {
            this.thissocket = (TcpClient)tcpClient;
        }

        public void run()
        {
            try
            {
                thissocket.NoDelay = true;
                NetworkStream thisIO = thissocket.GetStream();
                string host = Main.host;
                int port = Main.serverport;

                /************************** ↓↓↓↓↓ ******************************/

                /************************** ↑↑↑↑↑ ******************************/

                if (Main.forwardinfo)
                {
                    Main.form1.outText("R:" + thissocket.Client.RemoteEndPoint.ToString().Split(':')[0] + ":" + Convert.ToInt32(thissocket.Client.RemoteEndPoint.ToString().Split(':')[1]) + "\tF:" + host + ":" + port + "\r\n");
                }

                TcpClient sendsocket = new TcpClient() { NoDelay = true };
                sendsocket.Connect(host, port);
                NetworkStream sendIO = sendsocket.GetStream();

                /*********************** ↓↓↓↓↓ ***************************/

                /*********************** ↑↑↑↑↑ ***************************/

                Send send = new Send(thisIO, sendIO);
                Thread thread = new Thread(new ThreadStart(send.run)) { IsBackground = true };
                thread.Start();

                int i = sendIO.ReadByte();
                while (send.i != -1 && i != -1)
                {
                    thisIO.WriteByte((byte)Main.receivefun(i));
                    i = sendIO.ReadByte();
                }
            }
            catch (Exception e) { if (Main.errorinfo) { Main.form1.outText(e.Message.ToString() + "\r\n"); } }
        }
    }
}
