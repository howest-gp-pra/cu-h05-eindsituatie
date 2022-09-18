using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pra.Books.Core.Entities;
using Pra.Books.Core.Interfaces;
namespace Pra.Books.Core.Services
{
    public class BookServiceMem : IBookService
    {
        private List<Author> authors;
        private List<Publisher> publishers;
        private List<Book> books;


        public BookServiceMem()
        {
            Seeding();
        }
        private void Seeding()
        {
            authors = new List<Author>();
            publishers = new List<Publisher>();
            books = new List<Book>();

            authors.AddRange(new List<Author>
            {
                new Author (1,   "Elsschot Willem"),
                new Author (2,   "Boon Louis-Paul"),
                new Author (3,   "Claus Hugo"),
                new Author (4,   "Lanoye Tom"),
                new Author (5,   "Zinzen Walter"),
                new Author (6,   "Tuchman Barbara"),
                new Author (7,   "Christie Agatha"),
                new Author (8,   "Van Reybrouck David"),
                new Author (9,   "Pauwels Jan"),
                new Author (10,  "Konrad György"),
                new Author (11,  "Breemeersch Koen"),
                new Author (12,  "Jennings Roger"),
                new Author (13,  "Meyer Deon"),
                new Author (14,  "Jordan Camille"),
                new Author (15,  "Swan Tom"),
                new Author (16,  "Cook Robin"),
                new Author (17,  "Brown Dan"),
                new Author (18,  "Van Wittenberghe Annelies"),
                new Author (19,  "De Vos Danny"),
                new Author (20,  "Brusselmans Herman"),
                new Author (21,  "Van Aar Hetty")
            });
            publishers.AddRange(new List<Publisher>
            {
                new Publisher(1,   "Hadewijch"),
                new Publisher(4,   "Querido"),
                new Publisher(5,   "Arbeiderspers"),
                new Publisher(6,   "De Bezige Bij"),
                new Publisher(7,   "Prometheus"),
                new Publisher(8,   "QUE"),
                new Publisher(9,   "Academic Service"),
                new Publisher(10,  "Casterman"),
                new Publisher(11,  "AW Bruna"),
                new Publisher(12,  "Plantyn"),
                new Publisher(13,  "Luttingh"),
                new Publisher(88,  "Prometheus")
            });
            books.AddRange(new List<Book>
            {
                new Book( 1,   "Kaas"  ,  1 ,  4  , 1953),
                new Book(2,   "Jan De Lichte",   2 ,  5,   1962),
                new Book(3,   "Geuzenboek",  2 ,  5 ,  1964),
                new Book(4,   "Het verdriet van België", 3 ,  6 ,  1983),
                new Book(5,   "De geruchten",    3  , 6 ,  1996),
                new Book(6,   "De koele minnaar",    3 ,  6,   1970),
                new Book(7,   "Kartonnen dozen", 4  , 7 , 1993),
                new Book(8,   "Mobotu",  5  , 1 ,  1995),
                new Book(9,   "Programmeren met Turbo Pascal",   15,  9,   1995),
                new Book(10,  "Programmeren met C++",    15,  9 ,  1996),
                new Book(11,  "Programmeren met LISP",   15 , 9 ,  1995),
                new Book(12,  "Gestructureerde Analyse", 14,  9 ,  1989),
                new Book(13,  "OO software ontwerp", 14,  9,   1992),
                new Book(14,  "Compleet handboek Access 97", 12,  8 ,  1997),
                new Book(15,  "Compleet handboek Access 2000",   12 , 8 ,  1999),
                new Book(16,  "Compleet handboek Access 95", 12 , 8  , 1995),
                new Book(17,  "Tuinieren voor beginners",    11 , 7 ,  1999),
                new Book(18,  "Tuinieren voor gevorderden",  11 , 6 ,  1999),
                new Book(19,  "Afrikaanse tuinen",   11 , 6 ,  2000),
                new Book(20,  "Vreemd Lichaam",  16 , 11 , 2008),
                new Book(21,  "Het Juvenalis Dilemma",   17 , 13,  1998),
                new Book(22,  "Het verloren symbool",    17 , 13 , 2009),
                new Book(23,  "Revolusi",    8  , 6 ,  2020),
                new Book(24,  "Congo",   8 ,  6 ,  2010),
                new Book(25,  "Maanlicht van een andere planeet",    20,  7 ,  2010)
            });

        }
        public bool AddAuthor(Author author)
        {
            if(author.Id == 0)
            {
                int newId = 1;
                if (authors.Count > 0)
                 newId = authors.Max(a => a.Id);
                author.Id = newId + 1;
            }
            try
            {
                authors.Add(author);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddBook(Book book)
        {
            if (book.Id == 0)
            {
                int newId = 1;
                if (books.Count > 0)
                    newId = books.Max(a => a.Id);
                book.Id = newId + 1;
            }
            try
            {
                books.Add(book);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPublisher(Publisher publisher)
        {
            if (publisher.Id == 0)
            {
                int newId = 1;
                if (publishers.Count > 0)
                    newId = publishers.Max(a => a.Id);
                publisher.Id = newId;
            }
            try
            {
                publishers.Add(publisher);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAuthor(Author author)
        {
            if (IsAuthorInUse(author))
                return false;
            authors.Remove(author);
            return true;
        }
        public bool DeletePublisher(Publisher publisher)
        {
            if (IsPublisherInUse(publisher))
                return false;
            publishers.Remove(publisher);
            return true;
        }

        public bool DeleteBook(Book book)
        {
            books.Remove(book);
            return true;
        }

 
        public IEnumerable<Author> GetAuthors()
        {
            return authors.OrderBy(a => a.Name).ToList().AsReadOnly();
        }

        public IEnumerable<Book> GetBooks(Author author = null, Publisher publisher = null)
        {
            List<Book> filteredBooks = new List<Book>(books).OrderBy(b =>b.Title).ToList();
            if (author != null)
                filteredBooks = filteredBooks.Where(b => b.AuthorId == author.Id).ToList();
            if (publisher != null)
                filteredBooks = filteredBooks.Where(b => b.PublisherId == publisher.Id).ToList();
            return filteredBooks.AsReadOnly();

        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return publishers.OrderBy(a => a.Name).ToList().AsReadOnly();
        }

        public bool IsAuthorInUse(Author author)
        {
            if (books.Count(b => b.AuthorId == author.Id) == 0)
                return false;
            else
                return true;
        }

        public bool IsPublisherInUse(Publisher publisher)
        {
            if (books.Count(b => b.PublisherId == publisher.Id) == 0)
                return false;
            else
                return true;
        }

        public bool UpdateAuthor(Author author)
        {
            return true;
        }
        public bool UpdatePublisher(Publisher publisher)
        {
            return true;
        }
        public bool UpdateBook(Book book)
        {
            return true;
        }


    }
}
