using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Pra.Books.Core.Entities;
using Pra.Books.Core.Interfaces;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;


namespace Pra.Books.Core.Services
{
    public class BookServiceDB : IBookService
    {
        private readonly string constring 
            = @"Data Source=(local)\SQLEXPRESS;Initial Catalog = PraBooks; Integrated security = true;";
        private string HandleQuotes(string value)
        {
            return value.Trim().Replace("'", "''");
        }
        public IEnumerable<Author> GetAuthors()
        {
            string sql = "select id, name from authors order by name";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    return connection.Query<Author>(sql);
                }
                catch
                {
                    return null;
                }
            }
        }
        public IEnumerable<Publisher> GetPublishers()
        {
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    return connection.GetAll<Publisher>().OrderBy(p => p.Name);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IEnumerable<Book> GetBooks(Author author = null, Publisher publisher = null)
        {
            string sql = "Select * from books";
            List<string> filters = new List<string>();
            if (author != null)
                filters.Add("authorID = @AuthorID");
            if (publisher != null)
                filters.Add("publisherID = @PublisherID");
            if (filters.Count > 0)
                sql += $" where {string.Join(" and ", filters)}";
            sql += " order by title";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    return connection.Query<Book>(
                        sql,
                        new { AuthorID = author?.Id, PublisherID = publisher?.Id }
                    );
                }
                catch
                {
                    return null;
                }
            }

        }
        public bool IsAuthorInUse(Author author)
        {
            string sql = $"select count(*) from books where authorId = {author.Id}";
            //string sql = "select count(*) from books where authorID = @id";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    //int count = connection.ExecuteScalar<int>(sql, author);
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(sql, publisher);
                    return count > 0;
                }
                catch
                {
                    return true;
                }
            }
        }

        public bool AddAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    connection.Insert(author);
                    //var newAutoNumberValue = connection.Insert(publisher);
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    connection.Insert(book);
                    var newAutoNumberValue = connection.Insert(book);
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
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
