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
    public class BookRepository : GenericClass<Book>, IBook
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async void Add(BookDto bookDto)
        {
            var book = Entities(bookDto);
            if (book != null)
                await Add(book);
        }
        public List<Book> Get()
        {
          //  var bookData = _context.Books.Include(x => x.Authors).Include(x => x.Students).ToList();
            var bookData = GetAllIncluding(x=>x.Authors).ToList();
             bookData = GetAllIncluding(x=>x.Students).ToList();
            return bookData;
        }
        public Book GetById(int id)
        {
            // var GetBookById = _context.Books.Include(x => x.Authors).FirstOrDefault(a => a.Id == id);
            var GetBookById = GetAllIncluding(x => x.Authors).FirstOrDefault(x => x.Id == id);
            return GetBookById;
        }
        public void Delete(int id)
        {
           // var book = _context.Books.FirstOrDefault(x => x.Id == id);
            var book = GetById(id);
            if (book != null)
                Delete(book);
        }
        public Book Update(BookDto bookDto)
        {
         //   var book = _context.Books.FirstOrDefault(x => x.Id == bookDto.Id);
            var book = GetById(bookDto.Id);
            book.Name = bookDto.Name;
            book.Genre = bookDto.Genre;
            book.PublicationYear = bookDto.PublicationYear;
            book.stock = bookDto.stock;
            Update(book);
            return book;
        }
        private Book Entities(BookDto bookDto)
        {
            Book book = new Book();
            book.Name = bookDto.Name;
            book.Genre = bookDto.Genre;
            book.PublicationYear = bookDto.PublicationYear;
            book.stock = bookDto.stock;


            //var books = _context.Books.ToList();

            //foreach (var bookid in bookDto.BookIds)
            //{
            //    var book = (Book)books.Where(b => b.Id == bookid);
            //    author.Books.Add(book);
            //}
           

            return book;
        }
    }
}
