using System.Diagnostics;

namespace PA3_UE_Server
{
    public class GameManager
    {
        State State { get; set; }

        List<int> Field { get; set; }
        int Size { get; set; }
        int Mines { get; set; }

        NetworkManager networkManager;

        public GameManager(string IP = "")
        {
            networkManager = new NetworkManager(this, IP);
            State = State.Setup;
        }

        public void StartGame()
        {
            networkManager.StartServer();

            State = State.WaitingForResponse;
        }

        public void ConfigurationNegotiation(int size, int mines)
        {
            Debug.WriteLine("Negotiating Configuration");

            Size = size;
            Mines = mines;

            InitalizeField();

            State = State.WaitingForResponse;
        }

        public void RespondToShot(int field)
        {
            MSG msg = new MSG();
            msg.Type = PacketType.ShotResponse;

            // check for hits
            if (Field[field] == -1)
            {
                msg.HitMine = true;
                networkManager.TX.Send(msg);
                Field[field] = -2;
                CheckIfGameIsFinished();
                return;
            }

            Field[field] = 1;

            msg.HitMine = false;
            msg.NearbyMines = GetNearbyMines(field);
            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;

            CheckIfGameIsFinished();
        }

        private int GetNearbyMines(int cell)
        {
            return 5;
        }

        private void CheckIfGameIsFinished()
        {
            bool allCellsCleared = true;
            bool lost = false;
            // do some smart check
            foreach (int cell in Field)
            {
                if (cell == 0)
                {
                    allCellsCleared = false;
                }

                if (cell == -2)
                {
                    lost = true;

                    MSG msg = new MSG();
                    msg.Type = PacketType.Finish;
                    msg.Finished = false;
                    networkManager.TX.Send(msg);
                    break;
                }
            }

            if (!lost && allCellsCleared)
            {
                MSG msg = new MSG();
                msg.Type = PacketType.Finish;
                msg.Finished = true;
                networkManager.TX.Send(msg);
            }
        }

        private void InitalizeField()
        {
            Field = new List<int>(Size * Size);

            int placed_mines = 0;
            Random rnd = new Random();

            while (placed_mines < Mines)
            {
                int number = rnd.Next(0, Size * Size - 1);

                if (Field[number] != -1)
                {
                    Field[number] = -1;
                    placed_mines++;
                }
            }
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
