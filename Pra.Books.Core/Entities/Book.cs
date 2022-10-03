using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Books")]
    public class Book
    {
        private string title;

        [Key]
        public Guid Id { get; internal set; }

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Titel boek kan niet leeg zijn");
                if (value.Length > 100)
                    value = value.Substring(0, 100);
                title = value;
            }
        }

        public int Year { get; set; }

        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }

        public Book(string title, Guid authorId, Guid publisherId, int year)
        {
            Id = Guid.NewGuid();
            Title = title;
            AuthorId = authorId;
            PublisherId = publisherId;
            Year = year;
        }

        internal Book(Guid id, string title, Guid authorId, Guid publisherId, int year)
            : this(title, authorId, publisherId, year)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
