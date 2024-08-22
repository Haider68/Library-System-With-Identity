using DomainEntities.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBook
    {
        void Add(BookDto bookDto);
        List<Book> Get();
        Book GetById(int id);
        void Delete(int id);
        Book Update(BookDto bookDto);
    }
}
