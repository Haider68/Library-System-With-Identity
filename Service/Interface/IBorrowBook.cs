using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBorrowBook
    {
        string Add(int bookId, int studentId);
        string Return(int bookId, int studentId);
        //List<object> GetBorrowBooks(DateDto dateDto);
        List<object> GetReturnBook(DateDto dateDto);
        int GetBookStock(string name);
    }
}
