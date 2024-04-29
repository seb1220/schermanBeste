using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfChat
{
    internal class NetworkManager : Receiver
    {
        private MainWindow mainwindow;
        private Transfer transfer;

        public NetworkManager(MainWindow mainwindow, string username, string password, bool login)
        {
            this.mainwindow = mainwindow;
            ConnectToServer(username, password, login);
        }

        public void SendMessage(string chat, string message)
        {
            if (message == null) return;

            MSG msg = new MSG();
            msg.Type = MessageType.SendMessage;
            msg.RoomName = chat;
            msg.MessageConent = message;
            transfer.Send(msg);
        }

        public void CreateRoom(string roomName)
        {
            if (roomName == null) return;

            MSG msg = new MSG();
            msg.Type = MessageType.CreateRoom;
            msg.RoomName = roomName;
            transfer.Send(msg);
        }

        public void JoinRoom(string roomName)
        {
            if (roomName == null) return;

            MSG msg = new MSG();
            msg.Type = MessageType.JoinRoom;
            msg.RoomName = roomName;
            transfer.Send(msg);
        }

        private void ConnectToServer(string username, string password, bool login)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 42069);
            transfer = new Transfer(this, client);
            transfer.Start();

            MSG msg = new MSG();
            if (login)
                msg.Type = MessageType.Login;
            else
                msg.Type = MessageType.Register;
            msg.Username = username;
            msg.Password = password;
            transfer.Send(msg);

            Console.WriteLine($"Sent {msg.Type} message");
        }

        private void RegisterResponse(MSG msg)
        {
            if (!msg.AuthenticationSuccessful) {
                MessageBox.Show("Username already exists!");
            }

            FillChats(msg.Chats);
            FillAvailableChats(msg.AvailableChats);
        }

        private void LoginResponse(MSG msg)
        {
            if (!msg.AuthenticationSuccessful)
            {
                MessageBox.Show("Either password wrong or user does not exist!");
            }

            FillChats(msg.Chats);
            FillAvailableChats(msg.AvailableChats);
        }

        private void AddNewMessage(MSG msg)
        {
            Chat chat = mainwindow.Chats.Where(c => c.Name == msg.NewMessage.ChatRoom).First();
            if (chat != null)
            {
                chat.Messages.Add(msg.NewMessage.Message);
            }
        }

        private void AddNewChat(MSG msg)
        {
            mainwindow.AvailableChats.Add(msg.RoomName);
        }

        public void CloseConnection(Transfer transfer)
        {
            throw new NotImplementedException();
        }

        public void Receive(MSG msg, Transfer transfer)
        {
            Console.WriteLine($"Response received of Type {msg.Type}");

            if (msg == null)
                return;

            if (msg.Type == MessageType.LoginResponse) 
            { 
                LoginResponse(msg);
            } else if (msg.Type == MessageType.RegisterResponse)
            {
                RegisterResponse(msg);
            } else if (msg.Type == MessageType.NewMessge)
            {
                AddNewMessage(msg);
            } else if (msg.Type == MessageType.NewChat)
            {
                AddNewChat(msg);
            } else if (msg.Type == MessageType.JoinRoom)
            {
                FillChats(msg.Chats);
            }
        }

        private void FillChats(List<Chat> chats)
        {
            mainwindow.Chats.Clear();
            chats.ForEach(chat => { mainwindow.Chats.Add(chat); });
        }

        private void FillAvailableChats(List<string> chats)
        {
            chats.ForEach(c => Console.WriteLine("abailable chat: " + c));

            mainwindow.AvailableChats.Clear();
            chats.ForEach(chat => { mainwindow.AvailableChats.Add(chat); });

            mainwindow.AvailableChats.ToList().ForEach(c => Console.WriteLine("actually abailable chats: " + c));
        }
    }
}
