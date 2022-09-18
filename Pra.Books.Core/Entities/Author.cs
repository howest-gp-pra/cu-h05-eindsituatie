using System;
using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Authors")]
    public class Author
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
        public Author(string name)
        {
            Name = name;
        }
        internal Author(int id, string name)
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
