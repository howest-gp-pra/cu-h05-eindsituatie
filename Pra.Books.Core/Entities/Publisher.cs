using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Publishers")]
    public class Publisher
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
                    throw new Exception("Naam uitgeverij kan niet leeg zijn");
                if (value.Length > 100)
                    value = value.Substring(0, 100);
                name = value;
            }
        }

        public Publisher(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        internal Publisher(Guid id, string name) : this(name)
        {
            Id = id;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Publisher)
            {
                return false;
            }
            else
            {
                Publisher otherPublisher = (Publisher)obj;
                return otherPublisher.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
