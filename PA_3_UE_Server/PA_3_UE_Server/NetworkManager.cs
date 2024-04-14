using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace PA3_UE_Server
{
    internal class NetworkManager
    {
        int port = 42069;
        IPAddress ip;
        public ReceiverImpl RX { get; set; }
        public Transfer TX { get; set; }

        public NetworkManager(GameManager gm, string ip = "")
        {
            if (ip == "")
                this.ip = Dns.GetHostEntry("localhost").AddressList.First(); //this.ip = IPAddress.Parse("localhost");
            else
                this.ip = IPAddress.Parse(ip);

            RX = new ReceiverImpl(gm);

        }

        public void StartServer()
        {
            Debug.WriteLine($"Creating Server on: {ip} : {port}");
            TcpListener tcpListener = new TcpListener(ip, port);
            tcpListener.Start();

            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            Debug.WriteLine("Client connected ESTABLISHED");

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
