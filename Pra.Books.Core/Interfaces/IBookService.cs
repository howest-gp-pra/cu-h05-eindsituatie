using Pra.Books.Core.Entities;

namespace Pra.Books.Core.Interfaces
{   public interface IBookService
    {

        IEnumerable<Author> GetAuthors();
        bool AddAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool IsAuthorInUse(Author author);

        //Author FindAuthorByName(string name);
        //Author FindAuthorByID(string authorID);

        IEnumerable<Publisher> GetPublishers();
        bool AddPublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher publisher);
        bool IsPublisherInUse(Publisher publisher);

        //Publisher FindPublisherByName(string name);
        //Publisher FindPublisherByID(string publisherID);

        IEnumerable<Book> GetBooks(Author author, Publisher publisher);
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
    }
}
