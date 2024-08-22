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
    public class AuthorRepository:GenericClass<Author>,IAuthor
    {
        private readonly ApplicationDbContext _context;
     
        public AuthorRepository(ApplicationDbContext context) : base(context){}

        public void Add(AuthorDto authorDto)
        {
          var author =  Entities(authorDto);
            Add(author);
        }
        public List<Author> Get()
        {
            //var authorData = _context.Authors.Include(x=>x.Books).ToList();
            var authorData = GetAllIncluding(x=>x.Books).ToList();
            return authorData;
        }
        public Author GetById(int id)
        {
            var authorGetById = GetAllIncluding(x => x.Books).FirstOrDefault(a => a.Id == id);
            return authorGetById;
        }
        public Author Update(AuthorDto authorDto)
        {
           
         var updateAuthor = Entities(authorDto);
            if(updateAuthor != null)
            {
                Update(updateAuthor);
            }
           
            return updateAuthor;
        }
        public void Delete(int id)
        {
            var author = GetById(id);
            if(author != null)
            {
                Delete(author);
            }
        }
        public Author Entities(AuthorDto authorDto)
        {
            BookRepository bookRepository = new BookRepository(_context);
            if (authorDto.Id != 0)
            {
                var authorData = GetAllIncluding(x=>x.Books).FirstOrDefault(x => x.Id == authorDto.Id);
          
                
                authorData.Name = authorDto.Name;
                List<Book> bookList = new List<Book>();
              
                foreach (var bookid in authorDto.BookIds)
                {
                    if (bookid != 0)
                    {
                        var book = bookRepository.GetById(bookid);
                       bookList.Add(book);
                    }
                   
                }
                authorData.Books = bookList;
                authorData.AddressId = authorDto.AddessId;
                return authorData;
            }
            else
            {
                Author author = new Author();
                author.Name = authorDto.Name;
                List<Book> bookList = new List<Book>();
                foreach (var bookid in authorDto.BookIds)
                {
                    var book = bookRepository.GetById(bookid);
                    bookList.Add(book);
                }
                author.Books = bookList;
                author.AddressId = authorDto.AddessId;
                return author;
            }
        }
    }
}
