using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Books")]
    public class Book
    {
        private string title;
        private Author author;
        private Publisher publisher;

        [ExplicitKey]
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

        [Write(false)]
        public Author Author
        { 
            get { return author; }
            set
            {
                if(value == null)
                {
                    throw new Exception("Auteur is verplicht op te geven");
                }
                author = value;
            }
        }

        [Write(false)]
        public Publisher Publisher 
        { 
            get { return publisher; }
            set
            {
                if(value == null)
                {
                    throw new Exception("Uitgeverij is verplicht op te geven");
                }
                publisher = value;
            }
        }

        public Guid AuthorId
        {
            get { return Author.Id; }
        }

        public Guid PublisherId
        {
            get { return Publisher.Id; }
        }

        public Book(string title, Author author, Publisher publisher, int year) : this(Guid.NewGuid(), title, year)
        {
            Author = author;
            Publisher = publisher;
        }

        internal Book(Guid id, string title, int year)
        {
            Id = id;
            Title = title;
            Year = year;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
