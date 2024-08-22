using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.Model
{
    public class BorrowBooks
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int? BookId { get; set; }
        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Book Book { get; set; }
        public Student Student { get; set; }

    }
}
