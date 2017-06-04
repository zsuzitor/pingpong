using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace servpingpong
{

    public class lable11
    {
        public int left { get; set; }
        public int top { get; set; }

        public lable11()
        {
            left = 0;
            top = 0;
        }
    }

    public class lable22 : lable11
    {

    }
    public class ball : lable11
    {

    }

    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {


        }
        }

        class Program
    {


       



        static void Main(string[] args)
        {
            StreamReader reader;
            StreamWriter writer;
            StreamReader reader1;
            StreamWriter writer1;
            ball ball1 = new ball();
            lable11 player1 = new lable11();
            lable22 player2 = new lable22();

            ball1.left = 100;
            ball1.top = 100;

            int port = 12345;

            int panelH = 0;
            int panelW = 0;

            int speed_left = 4;
            int speed_top = 4;
            Console.WriteLine(@"1ip("")   2port("")");
            string ip1 = Console.ReadLine();
            if (ip1 == "")
               ip1 = "192.168.1.64";
            //ip1 = "127.0.0.1";

            // port =Convert.ToInt32( Console.ReadLine());
            string portTemp;
            portTemp = Console.ReadLine();
            port = portTemp == ""?12345:Convert.ToInt32( portTemp);
            
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip1), port));
            listener.Start();




            
            
                TcpClient server = listener.AcceptTcpClient();

            server.ReceiveTimeout = 50;
            reader = new StreamReader(server.GetStream());
            writer = new StreamWriter(server.GetStream());
            Console.WriteLine("1 подключился");
            // reader.ReadLine();
            writer.AutoFlush = true;

            TcpClient server1 = listener.AcceptTcpClient();

            server1.ReceiveTimeout = 50;
            reader1 = new StreamReader(server1.GetStream());
            writer1 = new StreamWriter(server1.GetStream());
            // reader.ReadLine();
            writer1.AutoFlush = true;
            Console.WriteLine("2 подключился");

            for (;;)
            {
                string hand;
                string[] hand2 = new string[10];

                string hand11;
                string[] hand21 = new string[10];
                try
                {

                    hand = reader.ReadLine();
                    string[] hand1 = hand.Split('#');
                    hand2 = hand1;

                    hand11 = reader1.ReadLine();
                    string[] hand12 = hand11.Split('#');
                    hand21 = hand12;

                    if (hand1.Length == 2)
                    {
                        panelH = Convert.ToInt32(hand1[0]);
                        panelW = Convert.ToInt32(hand1[1]);
                    }
                    if (hand != "")
                        Console.WriteLine(hand);
                    if (hand11 != "")
                        Console.WriteLine(hand11);


                }
                catch
                {
                    hand = "";
                    hand11 = "";
                }
                //сюда все
                if (hand2.Length == 3)
                {
                    if (hand2[2] == "1")
                    {
                        player1.left = Convert.ToInt32(hand2[0]);
                        player1.top = Convert.ToInt32(hand2[1]);
                    }
                    if (hand2[2] == "2")
                    {
                        player2.left = Convert.ToInt32(hand2[0]);
                        player2.top = Convert.ToInt32(hand2[1]);
                    }

                }
                if (hand21.Length == 3)
                {
                    if (hand21[2] == "1")
                    {
                        player1.left = Convert.ToInt32(hand21[0]);
                        player1.top = Convert.ToInt32(hand21[1]);
                    }
                    if (hand21[2] == "2")
                    {
                        player2.left = Convert.ToInt32(hand21[0]);
                        player2.top = Convert.ToInt32(hand21[1]);
                    }

                }


                ball1.left += speed_left;
                ball1.top += speed_top;



                if (ball1.top + 34 >= player1.top && ball1.top + 34 <= player1.top + 23 && ball1.left + 39 >= player1.left && ball1.left <= player1.left + 92)
                {
                    if (speed_left > -25 && speed_left < 25)
                        speed_left += 2;
                    if (speed_top > -25 && speed_top < 25)
                        speed_top += 2;
                    speed_top = -speed_top;



                }
                if (ball1.top <= player2.top + 23 && ball1.left + 39 >= player2.left && ball1.left <= player2.left + 92)
                {
                    if (speed_left > -25 && speed_left < 25)
                        speed_left -= 2;
                    if (speed_top > -25 && speed_top < 25)
                        speed_top -= 2;
                    speed_top = -speed_top;


                }


                if (ball1.left <= 0)
                    speed_left = -speed_left;
                if (ball1.left + 39 >= panelW)
                    speed_left = -speed_left;
                try
                {
                    if (ball1.top + 34 >= panelH)
                    {

                        //timer1.Enabled = false;

                        writer.WriteLine("выйграл игрок сверху");
                        writer1.WriteLine("выйграл игрок сверху");


                    }
                    if (ball1.top <= 0)
                    {



                        writer.WriteLine("выйграл игрок снизу");
                        writer1.WriteLine("выйграл игрок снизу");
                    }
                }
                catch
                {

                }




                try
                {


                    writer.WriteLine(ball1.left + "#" + ball1.top + "#" +player2.left);
                    writer1.WriteLine(ball1.left + "#" + ball1.top + "#" + player1.left);
                }
                catch
                {

                }


            }

        }




    }

}
