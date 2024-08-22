using DLA.Data;
using DomainEntities.Model;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Cryptography;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        public StudentController(IStudent student)
        {

            _student = student;
        }
        [HttpPost]
        public IActionResult Add(StudentDto studentDto)
        {
            try
            {
                _student.Add(studentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
        [HttpGet]
        public IActionResult Get()
        {
            var student = _student.GetStudent();
            return Ok(student);
        }
        [HttpPut]
        public IActionResult Update(StudentDto studentDto)
        {
            try
            {
                _student.Update(studentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _student?.Delete(id);
            return Ok();
        }
    }
}
