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
    public class BorrowBookRepository : IBorrowBook
    {
        private readonly ApplicationDbContext _context;
        public BorrowBookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Add(int bookId, int studentId)
        {

            BorrowBooks borrowBooks = new BorrowBooks();
            borrowBooks.BookId = bookId;
            borrowBooks.StudentId = studentId;
            borrowBooks.BorrowDate = DateTime.Today;
            _context.BorrowBooks.Add(borrowBooks);
            _context.SaveChanges();

            var bookdata = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (bookdata == null)
            {
                return "Book is Null";
            }
            bookdata.stock--;
            _context.Books.Update(bookdata);
            _context.SaveChanges();

            var student = _context.Students.FirstOrDefault(x => x.Id == studentId);
            if (student == null)
            {
                return "Student is Null";
            }
            List<Student> studentList1 = new List<Student>();
            studentList1.Add(student);

            var bookData = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (bookData == null)
            {
                return "Book is Null";
            }
            bookData.Students = studentList1;
            _context.Books.Update(bookData);
            _context.SaveChanges();

            return "data Add SuccessFully";
        }
        public string Return(int bookId, int studentId)
        {
            var borrowRecode = _context.BorrowBooks.FirstOrDefault(x => x.StudentId == studentId && x.BookId == bookId);
            if (borrowRecode == null)
            {
                return "this is not Borrow the book";
            }
            borrowRecode.ReturnDate = DateTime.Today;
            _context.BorrowBooks.Update(borrowRecode);
            _context.SaveChanges();

            var bookdata = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (bookdata == null)
            {
                return "This book is Null";
            }
            bookdata.stock++;
            _context.Books.Update(bookdata);
            _context.SaveChanges();

            var bookData = _context.Books.Include(x => x.Students).FirstOrDefault(x => x.Id == bookId);
            var student = _context.Students.FirstOrDefault(x => x.Id == studentId);
            if (bookData != null)
            {
                bookData.Students.Remove(student);
                _context.SaveChanges();
            }
            return null;
        }

        //public List<BorrowBooks> GetBorrowBooks(DateDto dateDto)
        //{
        //    var bookRecode = _context.BorrowBooks.Where(x => x.BorrowDate.Date >= dateDto.FromDate.Date && x.BorrowDate.Date <= dateDto.ToDate.Date).Select(x => new { x.BookId, x.StudentId }).ToList();
        //    //dateDto.FromDate = DateTime.Today;
        //    //dateDto.ToDate = DateTime.Today;
        //    //DayEndReport;
        //   return bookRecode;
        //}

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
