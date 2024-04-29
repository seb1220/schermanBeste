using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat
{
    public class Chat
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public Chat(string name) 
        {
            Name = name;
            Description = name;
            Messages = new ObservableCollection<Message>();
        }

        public Chat() { }
    }

    public class Message
    {
        public string Username { get; set; }
        public string Conent { get; set; }

        public Message(string username, string conent)
        {
            Username = username;
            Conent = conent;
        }

        public Message() { }

        public override string ToString()
        {
            return Username + ": " + Conent;
        }
    }
}
