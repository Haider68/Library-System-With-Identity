using DomainEntities.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthor
    {
        void Add(AuthorDto authorDto);
        List<Author> Get();
        Author GetById(int id);
        Author Entities(AuthorDto authorDto);
        void Delete(int id);
        Author Update(AuthorDto authorDto);
    }
}
