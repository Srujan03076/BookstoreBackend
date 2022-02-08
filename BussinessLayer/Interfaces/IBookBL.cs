using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IBookBL
    {
        BookModel AddBook(BookModel model);
        BookModel UpdateBook(BookModel model);
        bool DeleteBook(int bookId);
        BookModel GetBook(int BookId);
    }
}
