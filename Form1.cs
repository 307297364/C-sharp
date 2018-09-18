using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace startClient
{
    public partial class Form1 : Form
    {
        private Thread thread = null;
        public delegate void outDelegate(string str);

        public Form1()
        {
            InitializeComponent();
        }

        public void outText(string str)
        {
            if (this.textBox1.InvokeRequired)
            {
                outDelegate d = new outDelegate(outText);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                this.textBox4.AppendText(str);
                this.textBox4.SelectionStart = this.textBox4.TextLength;
                this.textBox4.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)//连接
        {
            Main.port = int.Parse(textBox1.Text);
            Main.host = textBox2.Text;
            Main.serverport = int.Parse(textBox3.Text);
            Main.forwardinfo = checkBox2.Checked;
            Main.errorinfo = checkBox1.Checked;

            if (Main.off)
            {
                this.button1.Enabled = false;
                this.textBox1.Enabled = false;
                this.textBox2.Enabled = false;
                this.textBox3.Enabled = false;
                this.checkBox1.Enabled = false;
                this.checkBox2.Enabled = false;
                thread = new Thread(new ThreadStart(Main.main)) { IsBackground = true };
                thread.Start();
                button2.Enabled = true;
            }
            else
            {
                this.outText("waiting...\r\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)//断开
        {
            Main.off = false;
            TcpClient tcpClient = new TcpClient("127.0.0.1", Main.port);
            button2.Enabled = false;
            this.button1.Enabled = true;
            this.textBox1.Enabled = true;
            this.textBox2.Enabled = true;
            this.textBox3.Enabled = true;
            this.checkBox1.Enabled = true;
            this.checkBox2.Enabled = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.textBox4.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Main.form1 = this;
            textBox1.Text = Main.port.ToString();
            textBox2.Text = Main.host;
            textBox3.Text = Main.serverport.ToString();
            checkBox2.Checked = Main.forwardinfo;
            checkBox1.Checked = Main.errorinfo;
            Thread.CurrentThread.IsBackground = true;
        }
    }
}
