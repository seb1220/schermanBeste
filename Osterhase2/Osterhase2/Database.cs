using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osterhase2
{
    internal class Database : DataConnection
    {
        public Database() : base("SQLite", "Data Source = test.db")
        {
            this.CreateTable<Person>(tableOptions:TableOptions.CheckExistence);
        }
    }
}
