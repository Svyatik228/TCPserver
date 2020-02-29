using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver
{
    class Network
    {
        bool ServerState = true;
        TcpClient client;
        public string ByteConvert(byte[]Resive)
        { int buf = Resive.Length;
            string r = " ";
            int f = 0;
            for(int i = 0; i < buf; i++)
            {
                if (Resive[i]!=0)
                {
                    f++;
                }
            }
            r = System.Text.Encoding.ASCII.GetString(Resive);
            r = r.Substring(0, f);
            return r;
        }
        public string SendRecive(string text,bool send){
            string response = " ";
            byte[] o = new byte[text.Length];
            try
            {
                switch (send)
                {
                    case true:
                        o = System.Text.Encoding.ASCII.GetBytes(text);
                        client.Client.Send(o);
                        break;
                    case false:
                        o = new byte[256];
                        response = ByteConvert(o);
                        break;
                }

            }
            catch
            {

            }
            return response;
        }
        void Listen()
        {
            IPAddress localAdress = IPAddress.Parse("0.0.0.0");
            int port = 911;
            TcpListener liss = new TcpListener(localAdress, port);
            liss.Start();
            while (ServerState)
            {
                client = liss.AcceptTcpClient();
                SendRecive("Hello",true);
                client.Client.Close();
            }
        }
        public void RunServer()
        {
            ServerState = true;
            Task S = new Task(Listen);
            S.Start();
        }
    }
}
