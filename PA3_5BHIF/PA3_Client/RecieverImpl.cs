using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA3_Client
{
    public class RecieverImpl : Receiver
    {

        private MainWindow mainWindow;
        
        public RecieverImpl(MainWindow main)
        {
            this.mainWindow = main;
        }

        public void ReceiveMessage(MSG m, Transfer t)
        {
            
            Debug.WriteLine("PACKET REC: " + m.Type);

            if (m.Type ==MessageType.NEW)
            {
                mainWindow.GameNumber = m.Config.GameNumber;
                Debug.WriteLine(mainWindow.GameNumber + " NUMBER RECEIVED");
            } else if(m.Type == MessageType.SAVE)
            {
                Debug.WriteLine(m.Nearby.Count);
                foreach(Mines mine in m.Nearby)
                {

                    int index = mine.X + mine.Y * mainWindow.Cols;
                    mainWindow.field[index].Value = mine.Count;
                   
                }

                mainWindow.lbField.Items.Refresh();

            } else if(m.Type == MessageType.EXPLODE)
            {
                MessageBox.Show("Verloren :( [Aufgabe sagt nur msg box]");
                
            }
        }

        public void TransferDisconnected(Transfer t)
        {
            Debug.WriteLine("Clsoed...");
            t.Stop();
        }
    }
}
