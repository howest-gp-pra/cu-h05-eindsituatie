﻿using Pra.Books.Core.Entities;
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
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000001"),   "Elsschot Willem"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000002"),   "Boon Louis-Paul"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000003"),   "Claus Hugo"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000004"),   "Lanoye Tom"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000005"),   "Zinzen Walter"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000006"),   "Tuchman Barbara"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000007"),   "Christie Agatha"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000008"),   "Van Reybrouck David"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000009"),   "Pauwels Jan"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000010"),  "Konrad György"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000011"),  "Breemeersch Koen"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000012"),  "Jennings Roger"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000013"),  "Meyer Deon"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000014"),  "Jordan Camille"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000015"),  "Swan Tom"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000016"),  "Cook Robin"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000017"),  "Brown Dan"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000018"),  "Van Wittenberghe Annelies"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000019"),  "De Vos Danny"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000020"),  "Brusselmans Herman"),
                new Author (Guid.Parse("00000000-0000-0000-0000-000000000021"),  "Van Aar Hetty")
            });

            publishers.AddRange(new List<Publisher>
            {
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000001"),   "Hadewijch"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000004"),   "Querido"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000005"),   "Arbeiderspers"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000006"),   "De Bezige Bij"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000007"),   "Prometheus"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000008"),   "QUE"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000009"),   "Academic Service"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000010"),  "Casterman"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000011"),  "AW Bruna"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000012"),  "Plantyn"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000013"),  "Luttingh"),
                new Publisher(Guid.Parse("00000000-0000-0000-0000-000000000088"),  "Prometheus")
            });

            books.AddRange(new List<Book>
            {
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000001"),   "Kaas"  ,                        Guid.Parse("00000000-0000-0000-0000-000000000001") ,  Guid.Parse("00000000-0000-0000-0000-000000000004")  , 1953),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000002"),   "Jan De Lichte",                 Guid.Parse("00000000-0000-0000-0000-000000000002") ,  Guid.Parse("00000000-0000-0000-0000-000000000005"),   1962),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000003"),   "Geuzenboek",                    Guid.Parse("00000000-0000-0000-0000-000000000002") ,  Guid.Parse("00000000-0000-0000-0000-000000000005") ,  1964),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000004"),   "Het verdriet van België",       Guid.Parse("00000000-0000-0000-0000-000000000003") ,  Guid.Parse("00000000-0000-0000-0000-000000000006") ,  1983),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000005"),   "De geruchten",                  Guid.Parse("00000000-0000-0000-0000-000000000004")  , Guid.Parse("00000000-0000-0000-0000-000000000006") ,  1996),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000006"),   "De koele minnaar",              Guid.Parse("00000000-0000-0000-0000-000000000003") ,  Guid.Parse("00000000-0000-0000-0000-000000000006"),   1970),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000007"),   "Kartonnen dozen",               Guid.Parse("00000000-0000-0000-0000-000000000004")  , Guid.Parse("00000000-0000-0000-0000-000000000007") , 1993),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000008"),   "Mobotu",                        Guid.Parse("00000000-0000-0000-0000-000000000005")  , Guid.Parse("00000000-0000-0000-0000-000000000001") ,  1995),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000009"),   "Programmeren met Turbo Pascal", Guid.Parse("00000000-0000-0000-0000-000000000015"),  Guid.Parse("00000000-0000-0000-0000-000000000009"),   1995),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000010"),  "Programmeren met C++",           Guid.Parse("00000000-0000-0000-0000-000000000015"),  Guid.Parse("00000000-0000-0000-0000-000000000009") ,  1996),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000011"),  "Programmeren met LISP",          Guid.Parse("00000000-0000-0000-0000-000000000015") , Guid.Parse("00000000-0000-0000-0000-000000000009") ,  1995),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000012"),  "Gestructureerde Analyse",        Guid.Parse("00000000-0000-0000-0000-000000000014"),  Guid.Parse("00000000-0000-0000-0000-000000000009") ,  1989),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000013"),  "OO software ontwerp", Guid.Parse("00000000-0000-0000-0000-000000000014"),  Guid.Parse("00000000-0000-0000-0000-000000000009"),   1992),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000014"),  "Compleet handboek Access 97", Guid.Parse("00000000-0000-0000-0000-000000000012"),  Guid.Parse("00000000-0000-0000-0000-000000000008") ,  1997),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000015"),  "Compleet handboek Access 2000",   Guid.Parse("00000000-0000-0000-0000-000000000012") , Guid.Parse("00000000-0000-0000-0000-000000000008") ,  1999),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000016"),  "Compleet handboek Access 95", Guid.Parse("00000000-0000-0000-0000-000000000012") , Guid.Parse("00000000-0000-0000-0000-000000000008")  , 1995),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000017"),  "Tuinieren voor beginners",    Guid.Parse("00000000-0000-0000-0000-000000000011") , Guid.Parse("00000000-0000-0000-0000-000000000007") ,  1999),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000018"),  "Tuinieren voor gevorderden",  Guid.Parse("00000000-0000-0000-0000-000000000011") , Guid.Parse("00000000-0000-0000-0000-000000000006") ,  1999),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000019"),  "Afrikaanse tuinen",   Guid.Parse("00000000-0000-0000-0000-000000000011") , Guid.Parse("00000000-0000-0000-0000-000000000006") ,  2000),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000020"),  "Vreemd Lichaam",  Guid.Parse("00000000-0000-0000-0000-000000000016") , Guid.Parse("00000000-0000-0000-0000-000000000011") , 2008),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000021"),  "Het Juvenalis Dilemma",   Guid.Parse("00000000-0000-0000-0000-000000000017") , Guid.Parse("00000000-0000-0000-0000-000000000013"),  1998),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000022"),  "Het verloren symbool",    Guid.Parse("00000000-0000-0000-0000-000000000017") , Guid.Parse("00000000-0000-0000-0000-000000000013") , 2009),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000023"),  "Revolusi",    Guid.Parse("00000000-0000-0000-0000-000000000008")  , Guid.Parse("00000000-0000-0000-0000-000000000006") ,  2020),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000024"),  "Congo",   Guid.Parse("00000000-0000-0000-0000-000000000008") ,  Guid.Parse("00000000-0000-0000-0000-000000000006") ,  2010),
                new Book(Guid.Parse("00000000-0000-0000-0000-000000000025"),  "Maanlicht van een andere planeet",    Guid.Parse("00000000-0000-0000-0000-000000000020"),  Guid.Parse("00000000-0000-0000-0000-000000000007") ,  2010)
            });
        }

        public bool AddAuthor(Author author)
        {
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
            return authors.OrderBy(a => a.Name);
        }

        public IEnumerable<Book> GetBooks(Author author = null, Publisher publisher = null)
        {

            // de LINK manier
            // we maken nieuwe List filteredBooks aan en
            // kopieren alle objecten uit de List books
            List<Book> filteredBooks = new List<Book>(books).OrderBy(b => b.Title).ToList();
            if (author != null)
                filteredBooks = filteredBooks.Where(b => b.AuthorId == author.Id).ToList();
            if (publisher != null)
                filteredBooks = filteredBooks.Where(b => b.PublisherId == publisher.Id).ToList();
            return filteredBooks.OrderBy(b => b.Title);

            // alternatieve manier
            // Indien geen enkele filter, dan wordt de List books geretourneerd
            // Anders maken we een nieuwe (lege) List filteredBooks aan en
            // overlopen alle objecten van de List books
            // en voegen indien van toepassing elk object toe 
            // aan de filteredBooks List

            //if (author == null && publisher == null)
            //    return books.OrderBy(b => b.Title);
            //else
            //{
            //    List<Book> filteredBooks = new List<Book>();
            //    foreach (Book book in filteredBooks)
            //    {
            //        if (author != null && publisher == null && author.Id == book.AuthorId)
            //            filteredBooks.Add(book);
            //        else if (publisher != null && author == null && publisher.Id == book.PublisherId)
            //            filteredBooks.Add(book);
            //        else if (author.Id == book.AuthorId && publisher.Id == book.PublisherId)
            //            filteredBooks.Add(book);
            //    }
            //    return filteredBooks.OrderBy(b => b.Title);
            //}
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return publishers.OrderBy(a => a.Name);
        }

        public bool IsAuthorInUse(Author author)
        {
            // de LINQ manier
            if (books.Count(b => b.AuthorId == author.Id) == 0)
                return false;
            else
                return true;

            //// alternatieve manier
            //foreach (Book book in books)
            //{
            //    if (book.AuthorId == author.Id)
            //        return true;
            //}
            //return false;
        }

        public bool IsPublisherInUse(Publisher publisher)
        {
            // de LINQ manier
            if (books.Count(b => b.PublisherId == publisher.Id) == 0)
                return false;
            else
                return true;

            //// alternatieve manier
            //foreach (Book book in books)
            //{
            //    if (book.PublisherId == publisher.Id)
            //        return true;
            //}
            //return false;
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
