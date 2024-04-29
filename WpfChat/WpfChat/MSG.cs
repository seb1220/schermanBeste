using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat
{
    public enum MessageType
    {
        // client
        Login,
        Register,
        SendMessage,
        CreateRoom,
        JoinRoom,

        // server
        LoginResponse,
        RegisterResponse,
        NewMessge,
        NewChat,
    }
    public class MSG
    {
        public MessageType Type { get; set; }

        // client / server
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoomName { get; set; }
        public string MessageConent { get; set; }
        public bool AuthenticationSuccessful { get; set; }
        public List<Chat> Chats { get; set; }
        public NewMessage NewMessage { get; set; }
        public List<string> AvailableChats { get; set; }

        public MSG() { }

    }

    public class NewMessage()
    {
        public string ChatRoom {  get; set; }
        public Message Message { get; set; }
    }
}
