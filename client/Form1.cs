using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace labirint
{
    public partial class Form1 : Form
    {

        StreamReader reader;
        StreamWriter writer;
        int port = 12345;
        string ip1 = "127.0.0.1";
        public bool player1 = true;
        public int speed_left = 4;
        public int speed_top = 4;
        public bool flag1 = false;
        
        public int point = 0;
        public string g = "";
        public int tip = 2;

        public Form1()
        {
            InitializeComponent();
            

            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            label1.Top = panel1.Bottom - (panel1.Bottom/10);
            

            
            
            
                label2.Width = 92;
                label2.Height = 23;
                label2.Left = panel1.Right / 2;
                label2.Show();



            
        }

 




        private void na4alo()
        {
            speed_top = 4;
            speed_left = 4;
            Cursor.Hide();
            label2.Show();
            

            ball.Left = 150;
            ball.Top = 150;
            
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player1)
            { 
            label1.Left = Cursor.Position.X - (label1.Width / 2);
            if (Cursor.Position.X - (label1.Width / 2) < panel1.Left)// && Cursor.Position.X + (label1.Width / 2) < panel1.Right)
                label1.Left = panel1.Left;
            if (Cursor.Position.X + (label1.Width / 2) > panel1.Right)
                label1.Left = panel1.Right - label1.Width - 10;
        }
            else
            {
                label2.Left = Cursor.Position.X - (label2.Width / 2);
                if (Cursor.Position.X - (label2.Width / 2) < panel1.Left)// && Cursor.Position.X + (label1.Width / 2) < panel1.Right)
                    label2.Left = panel1.Left;
                if (Cursor.Position.X + (label2.Width / 2) > panel1.Right)
                    label2.Left = panel1.Right - label2.Width - 10;
            }




            //надо передать мяч+ положение квадратов
            string zakid;
            if (player1)
            {
                zakid = label1.Left.ToString()+"#"+ label1.Top.ToString() + "#1";
                
            }
            else
            {
                zakid =  label2.Left.ToString() + "#" + label2.Top.ToString() + "#2";
            }
            writer.WriteLine(zakid);


             string hand = read1();
             string[] res = hand.Split('#');
            if (res.Count() == 1 )
            {
                if (res[0].IndexOf("выйграл") != -1)
                {
                    timer1.Enabled = false;
                    MessageBox.Show(res[0]);
                }
            }
            else
            {
                if (res.Count() == 3)
                {
                    ball.Left = Convert.ToInt32(res[0]);
                    ball.Top = Convert.ToInt32(res[1]);
                    if(player1)
                    {
                        label2.Left= Convert.ToInt32(res[2]);
                    }
                    else
                    {
                        label1.Left = Convert.ToInt32(res[2]);
                    }
                }
               
            }
            // ball.Left = Convert.ToInt32(res[0]);
            // ball.Top = Convert.ToInt32(res[1]);
            // label2.Left = Convert.ToInt32(res[3]);
            // label1.Left = Convert.ToInt32(res[2]);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                this.Close();
                

            }
            if (e.KeyCode == Keys.F1)
            {
                na4alo();
               
                flag1 = false;
                
                point=0;

            }
            
          

            //if (e.KeyCode == Keys.NumPad1)
           
            
            }


       
        private void startClient()
        {
            TcpClient client = new TcpClient();
            client.Connect(ip1, port);
            client.ReceiveTimeout = 50;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

        }

        private string read1()
        {
            try
            {
                string text;
                text = reader.ReadLine();

                return text;
            }
            catch
            {
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "2")
                player1 = false;

            ip1=textBox3.Text;
            port=Convert.ToInt32( textBox2.Text);
            textBox3.Visible = false;
            textBox2.Visible = false;
            // port = Convert.ToInt32( port1.Value);
            Cursor.Hide();
            textBox1.Visible = false;
            button1.Visible = false;
           // port1.Visible = false;
            timer1.Enabled = true;


            startClient();
            writer.WriteLine(panel1.Height + "#" + panel1.Width);
        }
    }
}
