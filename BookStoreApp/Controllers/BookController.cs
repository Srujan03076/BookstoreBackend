using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [HttpPost]
        public IActionResult AddBook(BookModel model)
        {
            try
            {
                BookModel result = this.bookBL.AddBook(model);
                if (result != null)
                {

                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Added New Book Successfully !", Data = result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new book" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });

            }
        }
        [HttpPut]
        public IActionResult UpdateBook(BookModel model)
        {
            try
            {
                BookModel result = this.bookBL.UpdateBook(model);
                if (result != null)
                {

                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Updation Successful!", Data = result });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Updation Failed" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });

            }
        }
        [HttpDelete]
        [Route("{bookId}/DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result == true)
                {

                    return this.Ok(new{ Success = true, Message = "Removed Book Successfully !"});
                }
                else
                {

                    return this.BadRequest(new{ Success = false, Message = "Failed to Remove Book" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });

            }
        }
        [HttpGet]
        [Route("{bookId}/GetBook")]
        public IActionResult GetBook(int BookId)
        {
           try
            {
                BookModel result = this.bookBL.GetBook(BookId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Book is retrived", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
    }
}

    



    

