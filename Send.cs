using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace startClient
{
    class Send
    {
        private NetworkStream inputStream;
        private NetworkStream outputStream;
        public int i = 0;

        public Send(NetworkStream inputStream, NetworkStream outputStream)
        {
            this.inputStream = inputStream;
            this.outputStream = outputStream;
        }

        public void run()
        {
            try
            {
                i = inputStream.ReadByte();
                while (i != -1)
                {
                    outputStream.WriteByte((byte)Main.forwardfun(i));
                    i = inputStream.ReadByte();
                }
            }
            catch (Exception e) { if (Main.errorinfo) { Main.form1.outText(e.Message.ToString() + "\r\n"); } }
        }
    }
}
