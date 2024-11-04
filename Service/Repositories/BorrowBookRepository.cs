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
using System.Xml.Linq;

namespace Service.Repositories
{
    public class BorrowBookRepository : GenericClass<BorrowBooks>,IBorrowBook
    {
        private readonly ApplicationDbContext _context;
       
        public BorrowBookRepository(ApplicationDbContext context):base (context)
        {
            _context = context;
        }
        public async Task<string> Add(int bookId, int studentId)
        {

            BorrowBooks borrowBooks = new BorrowBooks();
            borrowBooks.BookId = bookId;
            borrowBooks.StudentId = studentId;
            borrowBooks.BorrowDate = DateTime.Today;
            await Add(borrowBooks);

            BookRepository bookRepository = new BookRepository(_context);
           // var bookdata = _context.Books.FirstOrDefault(x => x.Id == bookId);
            var bookdata = bookRepository.GetById(bookId);
            if (bookdata == null)
            {
                return "Book is Null";
            }
            bookdata.stock--;
            
            bookRepository.Update(bookdata);
            //Update(bookdata);
            //_context.Books.Update(bookdata);
          StudentRepository studentRepository = new StudentRepository(_context);
            var student = studentRepository.GetById(studentId).Result; 
            if (student == null)
            {
                return "Student is Null";
            }
            List<Student> studentList1 = new List<Student>();
            studentList1.Add(student);

           // var bookData = _context.Books.FirstOrDefault(x => x.Id == bookId);
            var bookData = bookRepository.GetById(bookId);
            if (bookData == null)
            {
                return "Book is Null";
            }
            bookData.Students = studentList1;
            bookRepository.Update(bookData);
          

            return "data Add SuccessFully";
        }
        public string Return(int bookId, int studentId)
        {
            // var borrowRecode = _context.BorrowBooks.FirstOrDefault(x => x.StudentId == studentId && x.BookId == bookId);
            var borrowRecode = GetFirstOrDefault(x => x.StudentId == studentId && x.BookId==bookId);
            if (borrowRecode == null)
            {
                return "this is not Borrow the book";
               
            }
            borrowRecode.ReturnDate = DateTime.Today;
            Update(borrowRecode);
            //_context.BorrowBooks.Update(borrowRecode);
            //_context.SaveChanges();

            // var bookdata = _context.Books.FirstOrDefault(x => x.Id == bookId);
            BookRepository bookRepository = new BookRepository(_context);
            var bookdata = bookRepository.GetById(bookId);
            if (bookdata == null)
            {
                return "This book is Null";
            }
            bookdata.stock++;
            bookRepository.Update(bookdata);
            //_context.Books.Update(bookdata);
            //_context.SaveChanges();

            // var bookData = _context.Books.Include(x => x.Students).FirstOrDefault(x => x.Id == bookId);
            var bookData = bookRepository.GetAllIncluding(x => x.Students).FirstOrDefault(x => x.Id == bookId);
            StudentRepository studentRepository = new StudentRepository(_context);
            // var student = _context.Students.FirstOrDefault(x => x.Id == studentId);
            var student = studentRepository.GetById(studentId).Result;
            if (bookData != null)
            {
                studentRepository.Delete(student);
                //bookData.Students.Remove(student);
                //_context.SaveChanges();
            }
            return null;
        }

        public List<object> GetBorrowBooks(DateDto dateDto)
        {
            var BookBorrow = (from Student in _context.Students
                              join borrowBook in _context.BorrowBooks
                              on Student.Id equals borrowBook.StudentId
                              join book in _context.Books
                              on borrowBook.BookId equals book.Id

                              where (borrowBook.BorrowDate.Date >= dateDto.FromDate.Date &&
                              borrowBook.BorrowDate.Date <= dateDto.ToDate.Date)
                              group Student by book.Name into g
                              orderby g.Count() descending
                              select
                             new
                             {
                                 Books = g.Key,
                                 Count = g.Count(),
                             }).ToList<object>();
            //var bookRecode = _context.BorrowBooks
            //    .Where(x => x.BorrowDate.Date >= dateDto.FromDate.Date &&
            //    x.BorrowDate.Date <= dateDto.ToDate.Date)
            //    .Select(x => new { x.BookId, x.StudentId }).ToList();
            //DayEndReport;
            return BookBorrow;
        }

        public List<object> GetReturnBook(DateDto dateDto)
        {
            //dateDto.FromDate = DateTime.Today;
            //dateDto.ToDate = DateTime.Today;

            var BookReturn = (from Student in _context.Students
                              join borrowBook in _context.BorrowBooks
                              on Student.Id equals borrowBook.StudentId
                              join book in _context.Books
                              on borrowBook.BookId equals book.Id

                              where (borrowBook.ReturnDate.Date >= dateDto.FromDate.Date &&
                              borrowBook.ReturnDate.Date <= dateDto.ToDate.Date)
                              group Student by book.Name into g
                              orderby g.Count() descending
                              select
                             new
                             {
                                 Books = g.Key,
                                 Count = g.Count(),
                             }).ToList<object>();
            return BookReturn;
        }

        public int GetBookStock(string name)
        {
            var stocks = _context.Books.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
            if (stocks != null)
            {
                return stocks.stock;
            }
            else
            {
                return 0;
            }
        }
    }
}
