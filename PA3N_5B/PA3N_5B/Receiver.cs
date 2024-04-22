using System;
using System.Diagnostics;

namespace PA3N_5B
{
    public interface Receiver
    {
        void ReceiveMessage(MSG m, Transfer t);
        void TransferDisconnected(Transfer t);
    }

    public class ReceiverImpl : Receiver
    {
        GameManager gm;
        public void ReceiveMessage(MSG m, Transfer t)
        {
            Debug.WriteLine("RECEIVED MESSAGE:");
            Debug.WriteLine(m.ToString());

            gm.MatchConfirmedByServer(m.Word);
        }

        public void TransferDisconnected(Transfer t)
        {
            throw new NotImplementedException();
        }

        public ReceiverImpl(GameManager gm)
        {
            this.gm = gm;
        }
    }
}
