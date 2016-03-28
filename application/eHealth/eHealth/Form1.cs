using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;

using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace eHealth
{
    public partial class Form1 : Form
    {
        SerialPort com;
        bool isConnect;
       // delegate void HandleInterfaceUpdateDelegate(string text);  //委托，此为重点
       // HandleInterfaceUpdateDelegate interfaceUpdateHandle;
        Thread _readThread;
        bool _keepReading;




        enum LOCKER_STATUS{
            LOCK,
            UNLOCK
        };

        LOCKER_STATUS status;

        private System.Timers.Timer timer = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            com = new SerialPort();
            isConnect = false;
            

            //timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            //timer.Enabled = true;

 
            Console.WriteLine("serial port");

            String[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("no port ", "error");
                return;
            }
            foreach (String s in SerialPort.GetPortNames())
            {
                System.Console.WriteLine(s);
                serialBox.Items.Add(s);
            }
            serialBox.SelectedIndex = 0;
            button1.Text = "打开串口";
            label5.Text = "...";
            button3.Text = "...";
           // interfaceUpdateHandle = new HandleInterfaceUpdateDelegate(UpdateTextBox);  //实例化委托对象
            //this.com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.OnDataRecivice);
            com.ReceivedBytesThreshold = 1;

    


        }
        private void OnDataRecivice(object sender ,SerialDataReceivedEventArgs e) {
            byte[] readBuffer = new byte[com.ReadBufferSize];
            com.Read(readBuffer, 0, readBuffer.Length);
            //this.Invoke(interfaceUpdateHandle, new string[] { Encoding.Unicode.GetString(readBuffer) });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnect == false)
            {
                try
                {
                    com.BaudRate = 9600;
                    Console.WriteLine(serialBox.Text);
                    com.PortName = serialBox.Text;
                    com.DataBits = 8;
                    com.Open();
                    isConnect = true;
                    _keepReading = true;
                    _readThread = new Thread(ReadPort);
                    _readThread.Start();
                    button1.Text = "关闭串口";
                }
                catch (System.IO.IOException) {
                    button1.Text = "打开串口";
                    isConnect = false;
                    MessageBox.Show("打开串口失败", "失败");                
                }
                catch (System.NullReferenceException){
                    button1.Text = "打开串口";
                    isConnect = false;
                    MessageBox.Show("打开串口失败", "失败");
                }
            }
            else {
                _keepReading = false;
                isConnect = false;
                com.Close();
                button1.Text = "打开串口";
            }
        }

        /*private void button2_Click(object sender, EventArgs e)
        {
            com.WriteLine("1234");
            Console.WriteLine("1234");
        }*/
        private void UpdateTextBox(string text)
        {
            Console.WriteLine(text);
            
        }
        private void ReadPort() {
            while (_keepReading) {

                if (isConnect == true){

                    //byte[] readBuffer = new byte[com.ReadBufferSize + 1];
                    try
                    {

                        String SerialIn = com.ReadLine();
                        Console.WriteLine(SerialIn);

                        String sssss = SerialIn;
                        textBox1.Text = textBox1.Text + sssss + "\n\r";
                        textBox1.SelectionStart = textBox1.Text.Length - 1;
                        textBox1.ScrollToCaret();
                        //sssss = "AT+BT=60\r\n";

                        if (sssss == "lock\r") {
                            Console.WriteLine("锁关闭");
                            label5.Text = "锁关闭";
                            label5.BackColor = Color.Red;
                            status = LOCKER_STATUS.LOCK;
                            button3.Text = "打开";
                        }
                        else if(sssss == "unlock\r"){
                            Console.WriteLine("锁打开");
                            label5.Text = "锁打开";
                            label5.BackColor = Color.Green;
                            status = LOCKER_STATUS.UNLOCK;
                            button3.Text = "关闭";
                        }
                  

                        //HttpPost(value, longitude, latitude);

                    }
                    catch (TimeoutException) { }
                    catch (IOException) { }
                }
                else {
                    TimeSpan waitTime = new TimeSpan(0,0,0,50);
                    Thread.Sleep(waitTime);
                }
            }
        }
        




       /* private void button2_Click(object sender, EventArgs e)
        {
            //AT+ST=1
            //AT+BT=123
            //AT+LO=1234
            //AT+LA=123
            com.WriteLine("1234");
            Console.WriteLine("1234");
        }*/

  

        private void button2_Click(object sender, EventArgs e)
        {
            if (isConnect == true) {
                _keepReading = false;
                com.Close();
                isConnect = false;
            }
            this.Close();
            /*com.WriteLine("1234");
            Console.WriteLine("1234");*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (status == LOCKER_STATUS.LOCK)
            {
                button3.Text = "打开";
                com.WriteLine("UNLOCK");
            }
            else 
            {
                button3.Text = "关闭";
                com.WriteLine("TOLOCK");
            }
        }

        /*void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //模拟的做一些耗时的操作
            //Console.WriteLine("1234");
            HttpPost();
            System.Threading.Thread.Sleep(1000000);
            System.Threading.Thread.Sleep(1000000);
        }*/
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("你确定要关闭应用程序吗？", "关闭提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                //this.FormClosing -= new FormClosingEventHandler(this.Form1_FormClosing);//为保证Application.Exit();时不再弹出提示，所以将FormClosing事件取消
                Application.Exit();//退出整个应用程序
            }
            else
            {
                e.Cancel = true;  //取消关闭事件
            }
        }

    }
}
