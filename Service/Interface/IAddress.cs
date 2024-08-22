using DomainEntities.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAddress
    {
        Task<Address> Add(AddressDto addressDto);
        Task<List<Address>> GetAll();
        Task<Address> Get(int id);
        void Update(AddressDto addressDto);
        Task Delete(int id);
    }
}
