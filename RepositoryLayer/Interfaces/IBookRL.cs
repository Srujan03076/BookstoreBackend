using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        BookModel AddBook(BookModel model);
        BookModel UpdateBook(BookModel model);
        public  bool DeleteBook(int bookId);
        BookModel GetBook(int bookId);
    }
}
