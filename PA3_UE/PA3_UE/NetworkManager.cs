using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace PA3_UE
{
    internal class NetworkManager
    {
        int port = 42069;
        IPAddress ip;
        GameManager gm;
        public ReceiverImpl RX { get; set; }
        public Transfer TX { get; set; }

        public NetworkManager(GameManager gm, string ip = "")
        {
            this.gm = gm;
            if (ip == "")
                this.ip = Dns.GetHostEntry("localhost").AddressList.First(); //this.ip = IPAddress.Parse("localhost");
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
