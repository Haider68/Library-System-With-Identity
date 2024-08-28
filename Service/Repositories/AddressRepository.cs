using DLA.Data;
using DomainEntities.Model;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class AddressRepository : GenericClass<Address>, IAddress
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Address> Add(AddressDto addressDto)
        {
            try
            {
                var address = Entities(addressDto);
                if (address != null)
                    await Add(address);
                return address;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Address>> GetAll()
        {
            var address = GetAllIncluding(x => x.Student).ToList();
            //  var address = await _context.Addresses.Include(x=>x.Student).ToListAsync();
            return address;
        }
        public async Task<Address> Get(int id)
        {
            return await GetById(id);
        }
        public async void Update(AddressDto addressDto)
        {
            var address = Entities(addressDto);
            if (address != null)
                Update(address);
        }
        public async Task Delete(int id)
        {
            var address = GetById(id).Result;
            if (address != null)
            {
                await Delete(address);
            }

        }
        private Address Entities(AddressDto addressDto)
        {
            if (addressDto.Id == 0)
            {
                Address address = new Address();
                address.City = addressDto.City;
                address.Country = addressDto.Country;
                return address;
            }
            else
            {
                var address = GetById(addressDto.Id).Result;
                if (address != null)
                {
                    address.City = addressDto.City;
                    address.Country = addressDto.Country;
                }
                return address;
            }
        }
    }
}
