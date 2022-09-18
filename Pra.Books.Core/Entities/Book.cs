using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Pra.Books.Core.Entities
{
    [Table("Books")]
    public class Book
    {
        private string title;

        private int year;
        [Key]
        public int Id { get; internal set; }
        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Titel kan niet leeg zijn");
                if (value.Length > 100)
                    value = value.Substring(0, 100);
                title = value;
            }
        }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int Year
        {
            get { return year; }
            set
            {
                if (value < 0) value = 0;
                if (value > DateTime.Now.Year)
                    value = DateTime.Now.Year;
                year = value;
            }
        }
        public Book(string title, int authorId, int publisherId, int year)
        {
            Title = title;
            AuthorId = authorId;
            PublisherId = publisherId;
            Year = year;
        }
        internal Book(int id, string title, int authorId, int publisherId, int year)
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
