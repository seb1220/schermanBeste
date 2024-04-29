using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.models
{
    internal class ChatModel
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}
