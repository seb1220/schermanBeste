// See https://aka.ms/new-console-template for more information
using ChatServer;
using ChatServer.models;
using LinqToDB;
using System;
using System.Net;
using System.Net.Sockets;
using WpfChat;

Console.WriteLine("Hello, World!");
Main main = new Main();

public class Main
{
    public Database database;
    List<Transfer> transferList;

    public Main()
    {
        database = new Database();

        database.GetTable<UserModel>().ToList().ForEach(x => Console.WriteLine(x.Username + " " + x.Password));

        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 42069);
        listener.Start();

        transferList = new List<Transfer>();

        while (true)
        {
            Console.WriteLine("Waiting for connections");
            TcpClient client = listener.AcceptTcpClient();

            Console.WriteLine("New Client connected");
            ReceiverImpl receiver = new ReceiverImpl(database, this);
            Transfer transfer = new Transfer(receiver, client);
            transfer.Start();
            transferList.Add(transfer);
        }
    }

    public void SendMessage(string chat, string user, string content)
    {
        var users = database.GetTable<UserChatModel>().ToList().Where(uc => uc.Chat == chat).Select(uc => uc.Username).ToList();
        foreach (Transfer transfer in transferList)
        {
            if (!transfer.running)
                continue;

            if (!users.Contains(transfer.receiver.username))
                continue;
            
            MSG msg = new MSG();
            msg.Type = MessageType.NewMessge;
            msg.NewMessage = new NewMessage() { ChatRoom = chat, Message=new Message(user, content) };
            transfer.Send(msg);
        }
    }

    public void CreateChat(string chat)
    {
        foreach (Transfer transfer in transferList)
        {
            if (!transfer.running)
                continue;

            Console.WriteLine("sending new chat to " + transfer.receiver.username + " which is " + transfer.running);

            MSG msg = new MSG();
            msg.Type = MessageType.NewChat;
            msg.RoomName = chat;
            transfer.Send(msg);
        }
    }
}

public class ReceiverImpl
{
    Database database;
    Main main;
    public string username;

    public ReceiverImpl(Database database, Main main)
    {
        this.database = database;
        this.main = main;
    }

    public void CloseConnection(Transfer transfer)
    {
        Console.WriteLine("oohhh nooo, he disconnected :(");
    }

    public void Receive(MSG msg, Transfer transfer)
    {
        Console.WriteLine($"Received {msg.Type} message");
        
        if (msg.Type == MessageType.Register)
        {
            MSG response = new MSG();
            response.Type = MessageType.RegisterResponse;
            if (database.GetTable<UserModel>().Where(u => u.Username == msg.Username).Count() != 0) 
            {
                response.AuthenticationSuccessful = false;
                Console.WriteLine("Registration Declined!");
            } else
            {
                Console.WriteLine("Registration Successful!");
                username = msg.Username;
                response.AuthenticationSuccessful = true;
                response.AvailableChats = database.GetTable<ChatModel>().Select(c => c.Name).ToList();
            }
            transfer.Send(response);
        }
        else if (msg.Type == MessageType.Login)
        {
            Console.WriteLine($"Received: User: {msg.Username}, Password: {msg.Password}");
            database.GetTable<UserModel>().ToList().ForEach(u => Console.WriteLine($"DB: User: {u.Username}, Password: {u.Password}"));

            MSG response = new MSG();
            response.Type = MessageType.LoginResponse;
            if (database.GetTable<UserModel>().Where(u => u.Username == msg.Username && u.Password == msg.Password).Count() == 0)
            {
                response.AuthenticationSuccessful = false;
                Console.WriteLine("Login Declined!");
            }
            else
            {
                Console.WriteLine("Login Successful!");
                username = msg.Username;

                response.AuthenticationSuccessful = true;

                List<Chat> chatList = database.GetTable<UserChatModel>().ToList().Where(uc => uc.Username == username).Select(uc => new Chat(uc.Chat)).ToList();
                chatList.ForEach(cl => database.GetTable<MessageModel>().ToList()
                                            .Where(m => m.ChatName == cl.Name).ToList()
                                            .ForEach(m => cl.Messages.Add(new Message(m.Username, m.Content)))
                );
                Console.WriteLine("haaaaaaaaaaaalllo user: " + chatList[0].Messages[0].Username);

                response.AvailableChats = database.GetTable<ChatModel>().Select(c => c.Name).ToList();

                response.Chats = chatList;
            }
            transfer.Send(response);
        }
        else if (msg.Type == MessageType.CreateRoom)
        {
            if (username == null)
            {
                Console.WriteLine("Unauthorized User tryed to create chatroom");
                return;
            }

            if (database.GetTable<ChatModel>().Where(c => c.Name == msg.RoomName).Count() > 0)
            {
                Console.WriteLine("Authorized User tryed to create chat, that already exists");
                return;
            }

            Console.WriteLine("Creating new Chat with name " + msg.RoomName);
            database.Insert(new ChatModel() { Name = msg.RoomName });
            database.Insert(new UserChatModel() { Chat=msg.RoomName, Username=username });

            main.CreateChat(msg.RoomName);
        }
        else if (msg.Type == MessageType.SendMessage)
        {
            if (username == null)
            {
                Console.WriteLine("Unauthorized User tryed to send message");
                return;
            }

            Console.WriteLine("Sending new Message with content " + msg.MessageConent);

            database.Insert(new MessageModel() { ChatName = msg.RoomName, Content = msg.MessageConent, Username = username, DateTime = DateTime.Now });

            main.SendMessage(msg.RoomName, username, msg.MessageConent);
        } 
        else if (msg.Type == MessageType.JoinRoom)
        {
            if (username == null)
            {
                Console.WriteLine("Unauthorized User tryed to join chatroom");
                return;
            }

            Console.WriteLine("Joining Chat with name " + msg.RoomName);
            database.Insert(new UserChatModel() { Chat = msg.RoomName, Username = username });

            MSG response = new MSG();
            response.Type = MessageType.JoinRoom;
            List<Chat> chatList = database.GetTable<UserChatModel>().ToList().Where(uc => uc.Username == username).Select(uc => new Chat(uc.Chat)).ToList();

            chatList.ForEach(cl => database.GetTable<MessageModel>().ToList()
                .Where(m => m.ChatName == cl.Name).ToList()
                .ForEach(m => cl.Messages.Add(new Message(m.Username, m.Content)))
            );

            response.Chats = chatList;
            transfer.Send(response);
        }
    }
}
