using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication串口练习0928
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = sp.BytesToRead;
            byte[] buffer = new byte[count];
            sp.Read(buffer, 0, count);
            if(radioButton1.Checked)
            {
                textBox1.Text += byt(buffer);
            }
            else
            {
                textBox1.Text += Encoding.Default.GetString(buffer);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {












        }
        public static string byt(byte[] bytes)
        {
            string res = "";
            if(bytes !=null )
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    res += bytes[i].ToString("X2");
                }
            }
           
            return res;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        SerialPort sp = new SerialPort();
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] Name = SerialPort.GetPortNames();
            for (int i = 0; i <Name.Length; i++)
            {
                comboBox1.Items.Add(Name[i]);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!sp.IsOpen)
            {
                //sp.PortName = comboBox1.Text;
                sp.DataBits = int.Parse(comboBox3.Text);
                sp.BaudRate = int.Parse(comboBox2.Text);
                sp.StopBits = (StopBits)int.Parse(comboBox4.Text);
                sp.Parity = (Parity)int.Parse(comboBox5.Text);
                button1.Text = "关闭串口";
                sp.Open();
                sp.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            }
            else
            {
                sp.Close();
                button1.Text = "打开串口";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(sp.IsOpen)
            {
                if(radioButton1.Checked)
                {
                    string message = textBox2.Text;
                    byte[] buffer =  srtToHexByte(textBox2.Text);
                    sp.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    byte[] buffer = Encoding.Default.GetBytes(textBox2.Text);
                    sp.Write(buffer,0,buffer.Length);
                }


            }
            else
            {
                MessageBox.Show("串口没有正常打开");
            }

        }

        //转成16进制的方法
        public byte[] srtToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2 )!= 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length/2 ];//申请byte类型数组的空间
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2,2).Replace(" ", ""),16);
            }
            return returnBytes;
        }

            



    }
}
