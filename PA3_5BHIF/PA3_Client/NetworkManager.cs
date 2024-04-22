using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace PA3_Client
{
    [Serializable]
    internal class NetworkManager
    {
        int port = 12345;
        IPAddress ip;
        GameManager gm;
        public ReceiverImpl RX { get; set; }
        public Transfer TX { get; set; }

        public NetworkManager(GameManager gm, string ip = "")
        {
            this.gm = gm;
            if (ip == "")
                this.ip = IPAddress.Parse("127.0.0.1"); // this.ip = Dns.GetHostEntry("localhost").AddressList.First(); //
            else
                this.ip = IPAddress.Parse(ip);

            RX = new ReceiverImpl(gm);
        }

        public void ConnectClient()
        {
            Debug.WriteLine($"Connect client on {ip} : {port}");
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);

            TX = new Transfer(tcpClient, RX);
            TX.Start();
        }
    }
}
