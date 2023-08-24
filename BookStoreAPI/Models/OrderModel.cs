using BookStoreAPI.Data.Entities;

namespace BookStoreAPI.Models
{
    public class OrderModel
    {
        public Guid id { get; init; }
        public DateTime OrderDate { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
