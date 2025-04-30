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
using Microsoft.VisualBasic;

namespace WindowsFormsApp2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = "当前的地址：" + ip;
        }
        string ip = "ws://192.168.110.3:8181";
        private void button1_Click(object sender, EventArgs e)
        {
            var server = new WebSocketServer(ip);  //ws://localhost:8081    ws://127.0.0.0:8181  ws://192.168.0.146:8081
            label3.Text = "当前的地址："+ip;
            try
            {
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
            }
            catch (Exception ex) 
            {
                var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
                var str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",ex.GetType().Name, ex.Message, ex.StackTrace);
                MessageBox.Show("发生错误，这一般是您的Ip地址与本地地址不符，请检查您的Ip地址", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//进行弹窗提示
                Environment.Exit(0);
            }
           

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;

      

        private void button5_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入Ip地址", "Ip修改", "示例：ws://192.168.110.2:8181", -1, -1);
            if (str.Length == 0)
            {
                MessageBox.Show("没有输入,无效操作");
                return;
            }
            ip = str;
            label3.Text = "当前的地址：" + str;
            }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
