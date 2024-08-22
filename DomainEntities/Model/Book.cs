using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal PublicationYear { get; set; }
        public int stock { get; set; }
        public List<Author> Authors { get; set; }
        public List<Student> Students {  get; set; }  
        public List<BorrowBooks> BorrowBookList { get; set; }
      
    }
}
