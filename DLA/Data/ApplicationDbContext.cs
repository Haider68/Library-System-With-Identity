using DomainEntities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DLA.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>  //Identity
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<BorrowBooks>()
            //    .HasKey(bb => new { bb.BookId, bb.StudentId });
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresses { get; set; }
       public DbSet<BorrowBooks> BorrowBooks { get; set; }
    }

}
