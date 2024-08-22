using DLA.Data;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize/*(Roles ="Author")*/]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _author;
        public AuthorController(IAuthor author)
        {
            _author = author;
        }
        [HttpPost]
        public IActionResult Add(AuthorDto authorDto)
        {
            _author.Add(authorDto);
            return Ok(authorDto);
        }
        [HttpGet]
        public IActionResult Get()
        {
           var author = _author.Get();
            return Ok(author);
        }
        [HttpGet("Id")]
        public IActionResult Get(int Id)
        {
            var author = _author.GetById(Id);
            return Ok(author);
        }
        [HttpPut]
        public IActionResult Update(AuthorDto authorDto)
        {
            var authorData = _author.Update(authorDto);
            
            return Ok(authorData);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _author.Delete(id);
            return Ok();
        }
    }
}
