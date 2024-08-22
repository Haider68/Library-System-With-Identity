using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public  Address Address { get; set; }
        public List<Book> Books { get; set; }
        public List<BorrowBooks> BorrowBookList { get; set; }
       
    }
}
