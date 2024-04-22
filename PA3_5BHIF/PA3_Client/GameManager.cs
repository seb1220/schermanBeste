using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Xml.Serialization;

namespace PA3_Client
{
    [Serializable]
    public class GameManager
    {
        State State { get; set; }
        public GameField Field { get; set; }
        [XmlIgnore]
        System.Drawing.Point lastPick;
        public int GameNumber { get; set; } = 0;
        public string IP { get; set; }

        NetworkManager networkManager;

        public GameManager(GameField gameField, string ip = "")
        {
            State = State.Setup;
            Field = gameField;
            IP = ip;
            networkManager = new NetworkManager(this, ip);
        }

        public GameManager()
        {
        }

        public void StartGame(int width, int height, int mines)
        {
            Debug.WriteLine("Starting Game");
            Field.ClearField();
            networkManager.ConnectClient();

            MSG msg = new MSG();
            msg.Type = MessageType.NEW;

            Config config = new Config();
            config.Width = width;
            config.Height = height;
            config.Mines = mines;

            msg.Config = config;
            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;

            Debug.WriteLine("Game started");
        }

        public void GameConfirmedByServer(int gameNumber)
        {
            GameNumber = gameNumber;
            State = State.WaitingForUserInput;
        }

        public void Hit(int x, int y)
        {
            if (State != State.WaitingForUserInput)
                return;

            MSG msg = new MSG();
            msg.Type = MessageType.PICK;

            Pick pick = new Pick();
            pick.X = x; pick.Y = y;

            msg.Pick = pick;
            networkManager.TX.Send(msg);

            lastPick = new System.Drawing.Point(x, y);
            Debug.WriteLine($"Sent Hit: {msg}");

            State = State.WaitingForResponse;
        }

        public void HitResult(List<Mines> mines)
        {
            Debug.WriteLine("Receiving Result");
            int mineCount = 0;
            if (mines != null && mines.Count > 0)
            {
                mineCount = mines[0].Count;
            }

            Debug.WriteLine($"Hit Result: {mineCount}");

            Field.CellHit(lastPick, mineCount);

            State = State.WaitingForUserInput;
        }

        public void FinishedResult(bool won)
        {
            Debug.WriteLine("Received Finished Message");

            Field.CellHit(lastPick, -2);
            MessageBox.Show(won ? "You won!" : "You lost!");

            State = State.Finished;
            GameNumber = 0;
        }

        public void Reconnect()
        {
            if (networkManager == null)
                networkManager = new NetworkManager(this, IP);

            Debug.WriteLine("Reconnecting Game");
            networkManager.ConnectClient();

            MSG msg = new MSG();
            msg.Type = MessageType.NEW;

            Config config = new Config();
            config.Width = Field.Width;
            config.Height = Field.Height;
            config.Mines = Field.Mines;
            config.GameNumber = GameNumber;

            msg.Config = config;
            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;

            Debug.WriteLine("Reconnected");
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
