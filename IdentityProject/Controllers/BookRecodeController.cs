using DLA.Data;
using DomainEntities.Model;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRecodeController : ControllerBase
    {
        private readonly IBorrowBook _borrowBook;
        public BookRecodeController( IBorrowBook borrowBook)
        {
            
            _borrowBook = borrowBook;
        }
        [HttpPost("AddBorrowBooks")]
        public IActionResult Add(int bookId, int studentId)
        {
            try
            {
                string result = _borrowBook.Add(bookId, studentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("Return")]
        public IActionResult Return(int bookId, int studentId)
        {
            try
            {
                return Ok(_borrowBook.Return(bookId, studentId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetBorrowBooks")]
        public IActionResult get(DateDto dateDto)
        {
            try
            {
                //var GetBorrowBook = _borrowBook.GetBorrowBooks(dateDto);
                return Ok(/*GetBorrowBook*/);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetReturnBooks")]
        public IActionResult GetReturnBook(DateDto dateDto)
        {
            try
            {
                var GetReturnBook = _borrowBook.GetReturnBook(dateDto);
                return Ok(GetReturnBook);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("GetStock")]
        public IActionResult GetStock(string name)
        {
            try
            {
                return Ok(_borrowBook.GetBookStock(name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
