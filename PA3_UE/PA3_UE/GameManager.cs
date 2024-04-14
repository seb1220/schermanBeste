using System.Diagnostics;

namespace PA3_UE
{
    public class GameManager
    {
        State State { get; set; }
        GameField field;

        NetworkManager networkManager;

        public GameManager(GameField gameField, string IP = "")
        {
            State = State.Setup;
            field = gameField;
            networkManager = new NetworkManager(this, IP);
        }

        public void StartGame(int size)
        {
            Debug.WriteLine("Starting Game");
            networkManager.ConnectClient();

            MSG msg = new MSG();
            msg.Type = PacketType.Configuration;
            msg.Size = size;
            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;

            Debug.WriteLine("Game started");
        }

        public void Hit(int field)
        {
            if (State != State.WaitingForUserInput)
                return;

            Debug.WriteLine("Sending Hit");
            MSG msg = new MSG();
            msg.Type = PacketType.Shot;
            msg.Index = field;

            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;
        }

        public void HitResult(int index, bool mineHit, int nearbyMines)
        {
            Debug.WriteLine("Receiving Result");
            if (mineHit)
            {
                field.CellHit(index, CellType.Mine);
                return;
            }
            // dispay number of nearby mines
            field.CellHit(index, CellType.Cleared, nearbyMines);

            State = State.WaitingForUserInput;
        }

        public void FinishedResult(bool won)
        {
            Debug.WriteLine("Received Finished Message");
            // dispay game finished message to user

        }
    }

    internal enum State
    {
        Setup,
        WaitingForUserInput,
        WaitingForResponse,
        SendingResponse,
        Finished
    }
}
