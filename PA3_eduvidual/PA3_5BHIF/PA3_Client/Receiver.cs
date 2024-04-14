using System.Diagnostics;

namespace PA3_Client
{
    public interface Receiver
    {
        void ReceiveMessage(MSG m, Transfer t);
        void TransferDisconnected(Transfer t);
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
            Debug.WriteLine("RECEIVED MESSAGE:");
            switch (m.Type)
            {
                case MessageType.PICK:
                    Debug.WriteLine("pick");
                    break;
                case MessageType.NEW:
                    Debug.WriteLine("new");
                    break;
                case MessageType.SAVE:
                    Debug.WriteLine("save");
                    break;
                case MessageType.EXPLODE:
                    Debug.WriteLine("explo");
                    break;
            }
            Debug.WriteLine(m.ToString());
            Debug.WriteLine("Nearby: ", m.Nearby.Count);

            if (m.Type == MessageType.SAVE)
            {
                gm.HitResult(m.Nearby);
            }
            else if (m.Type == MessageType.EXPLODE)
            {
                gm.FinishedResult(false);
            }
            else if (m.Type == MessageType.NEW)
            {
                gm.GameConfirmedByServer(m.Config.GameNumber);
            }
        }

        public void TransferDisconnected(Transfer t)
        {
            t.Stop();
            //throw new NotImplementedException();
        }
    }
}
