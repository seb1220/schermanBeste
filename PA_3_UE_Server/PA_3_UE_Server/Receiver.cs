using System.Diagnostics;

namespace PA3_UE_Server
{
    public interface Receiver
    {
        void ReceiveMessage(MSG m, Transfer t);
        void TransferDisconnected(Transfer t);

        void AddDebugInfo(Transfer t, String m, bool sent);
    }

    public class ReceiverImpl : Receiver
    {
        GameManager gm;
        internal ReceiverImpl(GameManager gm)
        {
            this.gm = gm;
        }
        public void AddDebugInfo(Transfer t, string m, bool sent)
        {
            // throw new NotImplementedException();
        }

        public void ReceiveMessage(MSG m, Transfer t)
        {
            Debug.WriteLine("MType: ", m.Type);
            if (m.Type == PacketType.Configuration)
            {
                gm.ConfigurationNegotiation(m.Size, m.Mines);
            }
            else if (m.Type == PacketType.Shot)
            {
                gm.RespondToShot(m.Index);
            }
        }

        public void TransferDisconnected(Transfer t)
        {
            t.Stop();
            //throw new NotImplementedException();
        }
    }

}
