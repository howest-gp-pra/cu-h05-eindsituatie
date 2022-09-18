using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Publishers")]
    public class Publisher
    {
        private string name;
        [Key]
        public int Id { get; internal set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length > 100)
                    value = value.Substring(0, 100);
                name = value;
            }
        }
        public Publisher(string name)
        {
            Name = name;
        }
        internal Publisher(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
