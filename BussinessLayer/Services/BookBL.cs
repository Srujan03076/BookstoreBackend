using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;

        }

        public BookModel AddBook(BookModel model)
        {
            try
            {
                return this.bookRL.AddBook(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public BookModel GetBook(int bookId)
        {
            try
            {
                return this.bookRL.GetBook(bookId);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public BookModel UpdateBook(BookModel model)
        {
            try
            {
                return this.bookRL.UpdateBook(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

        
    

