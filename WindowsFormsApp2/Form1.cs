using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fleck;

namespace WindowsFormsApp2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var server = new WebSocketServer("ws://192.168.110.2:8181");  //ws://localhost:8081    ws://127.0.0.0:8181  ws://192.168.0.146:8081
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Debug.WriteLine($"有新用户连入：{socket.ConnectionInfo.ClientIpAddress}");
                };
                socket.OnClose = () =>
                {
                    Debug.WriteLine($"用户断开连接：{socket.ConnectionInfo.ClientIpAddress}");
                };
                socket.OnMessage = message =>
                {
                    socket.Send($"服务器收到消息 : {DateTime.Now.ToString()}");
                    Debug.WriteLine($"收到一条消息，来自：{socket.ConnectionInfo.ClientIpAddress}");
                    if (label2.InvokeRequired)
                    {
                        Action SetText111 = delegate { SetText($"服务器收到消息 : {DateTime.Now.ToString()}" + $"收到一条消息，来自：{socket.ConnectionInfo.ClientIpAddress}"); };
                        label2.Invoke(SetText111);
                    }
                    else
                    {
                        label2.Text = $"服务器收到消息 : {DateTime.Now.ToString()}" + $"收到一条消息，来自：{socket.ConnectionInfo.ClientIpAddress}";
                    }

                    Writr_Folider(message);
                };
            });

            Debug.WriteLine("服务器已经启动！");
            label1.Text = "服务器已经启动！";
            
        }
        public void SetText(string SText)
        {
            label2.Text = SText;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender,e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void Writr_Folider(string str)
        {
            //if (!File.Exists("log.txt"))
            //{
            //    FileStream fs = new FileStream("log.txt", FileMode.Create, FileAccess.Write);
            //}
             File.AppendAllText("log.txt", "\r\n" + str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("log.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {

          DialogResult dr = MessageBox.Show("是否清空已写入的文件数据信息？", "操作警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dr== DialogResult.OK)
            {
                System.IO.File.WriteAllText("log.txt", string.Empty);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
