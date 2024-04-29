using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WpfChat
{
    public class Transfer
    {
        public ReceiverImpl receiver;
        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        private NetworkStream stream;
        private XmlSerializer serializer;

        public Transfer(ReceiverImpl r, TcpClient c) 
        {
            receiver = r;
            client = c;
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            serializer = new XmlSerializer(typeof(MSG));
        }

        Thread t;
        public bool running;

        public void Start()
        {
            t = new Thread(Run);
            running = true;
            t.IsBackground = true;
            t.Start();
        }

        public void Stop()
        {
            running = false;
            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        private void Run()
        {
            string msg = "";
            while (running && client.Connected)
            {
                try {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Length == 0)
                        continue;

                    msg += line + "\n";
                    if (msg.Contains("</MSG>"))
                    {
                        StringReader stringReader = new StringReader(msg);

                        receiver.Receive((MSG)serializer.Deserialize(stringReader), this);

                        msg = "";
                    }

                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            receiver.CloseConnection(this);
            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        public void Send(MSG msg)
        {
            StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, msg);

            writer.WriteLine(stringWriter.ToString());
            writer.Flush();
        }
    }
}
