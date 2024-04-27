using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osterhase2
{
    [Table]
    internal class Person
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
