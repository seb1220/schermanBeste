using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.models
{
    internal class UserModel
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
