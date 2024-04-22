using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace PA3_Klauninger;

public class TransferManager {
    private readonly TcpClient _client;
    private readonly Action _onDisconnect;
    private readonly Action<MSG> _onReceive;
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;
    private readonly XmlSerializer _xml;
    private readonly Logger _logger;

    public TransferManager(TcpClient client, Action<MSG> onReceive,
        Action onDisconnect) {
        _client = client;
        _onReceive = onReceive;
        _onDisconnect = onDisconnect;

        var stream = client.GetStream();
        _reader = new StreamReader(stream);
        _writer = new StreamWriter(stream) { AutoFlush = true };
        _xml = new XmlSerializer(typeof(MSG));
        _logger = new Logger("NETWORK");

        _ = ThreadPool.QueueUserWorkItem(_ => Receive());
    }

    ~TransferManager() {
        Close();
    }

    public void Close() {
        _client.Close();
    }

    public void Send(MSG data) {
        _logger.Info("Sending message: [" + data + "]");
        StringWriter messageStream = new();
        _xml.Serialize(messageStream, data);
        _writer.WriteLine(messageStream.ToString());
    }

    private void Receive() {
        var xmlString = "";
        while (_client.Connected) {
            try {
                var line = _reader.ReadLine();
                if (line == null) {
                    break;
                }

                xmlString += line;
            }
            catch (IOException) {
                break;
            }

            if (!xmlString.EndsWith("</" + nameof(MSG) + ">")) {
                continue;
            }

            var receivedMessage = (MSG)_xml.Deserialize(new StringReader(xmlString))!;

            _logger.Info("Received message: [" + receivedMessage + "]");
            _onReceive(receivedMessage);

            xmlString = "";
        }

        _onDisconnect();
    }

    public class MSG {
        public bool? NewGame { get; set; } = true;
        public string? Word { get; set; } = null;

        public override string ToString() {
            return $"NewGame={NewGame};Word={Word}";
        }
    }
}