using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Authors")]
    public class Author
    {
        private string name;

        [ExplicitKey]
        public Guid Id { get; }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Naam auteur kan niet leeg zijn");
                if (value.Length > 100)
                    value = value.Substring(0, 100);
                name = value;
            }
        }

        public Author(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        internal Author(Guid id, string name) : this(name)
        {
            Id = id;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            return obj is Author author &&
                   Id.Equals(author.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
