using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.models
{
    internal class MessageModel
    {
        [Identity, PrimaryKey]
        public int Id { get; set; }
        public string ChatName { get; set; }
        public DateTime DateTime { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
