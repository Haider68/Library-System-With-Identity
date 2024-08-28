using DLA.Data;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace IdentityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _address;
        public AddressController(IAddress address)
        {
            _address = address;
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddressDto addressDto)
        {
            try
            {
                await _address.Add(addressDto);
                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var addess = await _address?.GetAll();
            return Ok(addess);
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _address.Get(id);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(AddressDto addressDto)
        {
            try
            {
                _address?.Update(addressDto);
                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await _address?.Delete(id);
            return Ok();
        }
    }
}
