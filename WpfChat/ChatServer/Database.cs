using ChatServer.models;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Database : DataConnection
    {

        public Database() : base("SQLite", "Data Source = WPFChat.db") 
        { 
            this.CreateTable<UserModel>(tableOptions:TableOptions.CheckExistence);
            this.CreateTable<ChatModel>(tableOptions:TableOptions.CheckExistence);
            this.CreateTable<UserChatModel>(tableOptions:TableOptions.CheckExistence);
            this.CreateTable<MessageModel>(tableOptions:TableOptions.CheckExistence);
        }
    }
}
