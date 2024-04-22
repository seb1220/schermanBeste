using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Xml.Serialization;

namespace PA3N_5B
{
    [Serializable]
    public class GameManager
    {
        public State State { get; set; }
        //public GameField Field { get; set; }
        [XmlIgnore]
        System.Drawing.Point lastPick;
        public string Word { get; set; } = "";
        public string IP { get; set; }

        ObservableCollection<Cell> cells;

        NetworkManager networkManager;

        public GameManager(ObservableCollection<Cell> cells, string ip = "")
        {
            State = State.Setup;
            this.cells = cells;
            IP = ip;
            networkManager = new NetworkManager(this, ip);
        }

        public void StartGame()
        {
            Debug.WriteLine("Starting Game");
            //Field.ClearField();
            networkManager.ConnectClient();

            MSG msg = new MSG();
            msg.NewGame = true;

            networkManager.TX.Send(msg);

            State = State.WaitingForResponse;

            Debug.WriteLine("Game started");
        }

        public void MatchConfirmedByServer(string word)
        {
            Word = word;
            cells.Clear();
            for (int i = 0; i < 30; i++)
            {
                cells.Add(new Cell());
            }
            State = State.WaitingForUserInput;
        }

        public void Response(bool newGame, string word)
        {
            Debug.WriteLine("Receiving Result");


            Debug.WriteLine($"Result: {newGame} {word}");


            State = State.WaitingForUserInput;
        }

        public void Finish(bool won)
        {
            MessageBox.Show(won ? "You won!" : "You lost!");
            State = State.Finished;
        }

    }

    public enum State
    {
        Setup,
        WaitingForUserInput,
        WaitingForResponse,
        SendingResponse,
        Finished
    }
}
