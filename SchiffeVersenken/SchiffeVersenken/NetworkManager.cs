using System;
using System.Net;
using System.Net.Sockets;

namespace SchiffeVersenken
{
    internal class NetworkManager
    {
        bool isServer = false;
        int port = 42069;
        IPAddress ip;
        public NetworkManager(bool server, string ip = "")
        {
            isServer = server;
            if (ip == "")
                this.ip = IPAddress.Parse(GetLocalIPAddress());
            else
                this.ip = IPAddress.Parse(ip);
        }

        void StartServer()
        {
            TcpListener tcpListener = new TcpListener(ip, port);
            tcpListener.Start();
        }

        void StartClient()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
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
