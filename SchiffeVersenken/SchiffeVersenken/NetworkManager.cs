using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace SchiffeVersenken
{
    internal class NetworkManager
    {
        int port = 42069;
        IPAddress ip;
        GameManager gm;
        public ReceiverImpl RX { get; set; }
        public Transfer TX { get; set; }

        public NetworkManager(GameManager gm, bool isServer, string ip = "")
        {
            this.gm = gm;
            if (ip == "")
                this.ip = IPAddress.Parse(GetLocalIPAddress());
            else
                this.ip = IPAddress.Parse(ip);

            RX = new ReceiverImpl(gm);

            if (isServer)
                StartServer();
            else
                ConnectClient();
        }

        void StartServer()
        {
            Debug.WriteLine($"Creating Server on: {ip} : {port}");
            TcpListener tcpListener = new TcpListener(ip, port);
            tcpListener.Start();

            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            Debug.WriteLine("Client connected ESTABLISHED");

            TX = new Transfer(tcpClient, RX);
            TX.Start();
        }

        void ConnectClient()
        {
            Debug.WriteLine($"Connect client on {ip} : {port}");
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);

            TX = new Transfer(tcpClient, RX);
            TX.Start();
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
