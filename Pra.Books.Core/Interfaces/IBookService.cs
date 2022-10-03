﻿using Pra.Books.Core.Entities;

namespace Pra.Books.Core.Interfaces
{   
    public interface IBookService
    {
        IEnumerable<Author> GetAuthors();
        bool AddAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool IsAuthorInUse(Author author);

        IEnumerable<Publisher> GetPublishers();
        bool AddPublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher publisher);
        bool IsPublisherInUse(Publisher publisher);

        IEnumerable<Book> GetBooks(Author author = null, Publisher publisher = null);
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
    }
}
