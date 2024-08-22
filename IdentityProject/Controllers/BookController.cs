using DomainEntities.Model;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;
        public BookController(IBook book)
        {
            _book = book;
        }
        [HttpPost]
        public IActionResult Add(BookDto bookDto)
        {
            _book.Add(bookDto);
            return Ok(bookDto);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var book = _book.Get();
            return Ok(book);
        }
        [HttpGet("Id")]
        public IActionResult Get(int Id)
        {
            var book = _book.GetById(Id);
            return Ok(book);
        }
        [HttpPut]
        public IActionResult Update(BookDto bookDto)
        {

            return Ok(bookDto);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _book.Delete(id);
            return Ok();
        }

    }
}
