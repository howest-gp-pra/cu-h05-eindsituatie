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
            if(obj is not Author)
            {
                return false;
            }
            else
            {
                Author otherAuthor = (Author)obj;
                return otherAuthor.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
