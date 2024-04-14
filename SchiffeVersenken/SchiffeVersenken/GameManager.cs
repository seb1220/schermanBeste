using System.Collections.Generic;
using System.Diagnostics;

namespace SchiffeVersenken
{
    public class GameManager
    {
        State State { get; set; }

        NetworkManager networkManager;

        public GameManager()
        {
            State = State.Setup;
        }

        public void StartGame(bool isServer, string IP = "")
        {
            networkManager = new NetworkManager(this, isServer, IP);

            if (!isServer)
            {
                MSG msg = new MSG();
                msg.Type = PacketType.Configuration;
                msg.Start = 1;
                msg.Size = 6;
                msg.TwoShips = 4;
                msg.ThreeShips = 3;
                msg.FourShips = 2;
                msg.FiveShips = 1;
                msg.ShipList = new List<int> { 0, 1, 0 };

                networkManager.TX.Send(msg);
            }

            State = State.WaitingForResponse;
        }

        public void ConfigurationNegotiation(MSG m)
        {
            Debug.WriteLine("Negotiating Configuration");
            // check if all configurations are equal

            State = State.WaitingForUserInput;
        }

        public void Shoot(int field)
        {
            MSG msg = new MSG();
            msg.Type = PacketType.Shot;
            msg.Index = field;

            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;
        }

        public void RespondToShot(int field)
        {
            // check for hits

            MSG msg = new MSG();
            msg.Type = PacketType.ShotResponse;
            msg.Hit = true;
            msg.Sunk = true;

            networkManager.TX.Send(msg);

            State = State.WaitingForUserInput;
        }

        public void CheckIfGameIsFinished()
        {
            // do some smart check

            if (1 > 2)
            {
                MSG msg = new MSG();
                msg.Type = PacketType.Finish;
                msg.Winner = 0; // fix
                networkManager.TX.Send(msg);
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
