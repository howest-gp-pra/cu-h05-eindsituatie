using Pra.Books.Core.Entities;
using Pra.Books.Core.Interfaces;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;


namespace Pra.Books.Core.Services
{
    public class BookServiceDB : IBookService
    {
        private readonly string conString 
            = @"Data Source=(local)\SQLEXPRESS;Initial Catalog = PraBooks; Integrated security = true;";

        public IEnumerable<Author> GetAuthors()
        {
            string sql = "select id, name from authors order by name";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    return connection.Query<Author>(sql);
                }
                catch
                {
                    return Enumerable.Empty<Author>();
                }
            }
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            string sql = "select id, name from publishers order by name";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    return connection.Query<Publisher>(sql);
                }
                catch
                {
                    return Enumerable.Empty<Publisher>();
                }
            }
        }

        public IEnumerable<Book> GetBooks(Author author = null, Publisher publisher = null)
        {
            // SQL query met join wegens compositie
            string sql = "Select b.Id, b.Title, b.Year, a.*, p.* from books b ";
            sql += "inner join Authors a on b.AuthorID = a.ID ";
            sql += "inner join Publishers p on b.PublisherID = p.ID ";

            // filter(s) toevoegen aan SQL query
            List<string> filters = new List<string>();
            if (author != null)
                filters.Add($"authorID = '{author.Id}'");
            if (publisher != null)
                filters.Add($"publisherID = '{publisher.Id}'");
            if (filters.Count > 0)
                sql += $" where {string.Join(" and ", filters)}";
            sql += " order by title";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    return connection.Query<Book, Author, Publisher, Book>(sql, (book, author, publisher) =>
                    {
                        book.Author = author;
                        book.Publisher = publisher;
                        return book;
                    });
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool IsAuthorInUse(Author author)
        {
            string sql = $"select count(*) from books where authorId = '{author.Id}'";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    int count = connection.ExecuteScalar<int>(sql);
                    return count > 0;
                }
                catch
                {
                    // als er zich een fout voordoet dan retourneren we TRUE
                    // deze methode zal gebruikt worden voor een boek verwijderd
                    // wordt, dus is het veiliger om aan te geven dat de auteur nog
                    // in gebruik is bij een fout, zodat er geen boek kan 
                    // verwijderd worden
                    return true;
                }
            }
        }

        public bool IsPublisherInUse(Publisher publisher)
        {
            string sql = "select count(*) from books where publisherId = @id";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    int count = connection.ExecuteScalar<int>(sql, publisher);
                    return count > 0;
                }
                catch
                {
                    // zelfde reden als bij IsAuthorInUse
                    return true;
                }
            }
        }

        public bool AddAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Insert(author);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool AddPublisher(Publisher publisher)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Insert(publisher);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Insert(book);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Update(author);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Update(publisher);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Update(book);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteAuthor(Author author)
        {
            if (IsAuthorInUse(author))
                return false;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Delete(author);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeletePublisher(Publisher publisher)
        {
            if (IsPublisherInUse(publisher))
                return false;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Delete(publisher);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Delete(book);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }       
    }
}
