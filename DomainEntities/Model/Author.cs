namespace DomainEntities.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public List<Book> Books { get; set; }
    }
}
