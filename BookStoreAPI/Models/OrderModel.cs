using BookStoreAPI.Data.Entities;

namespace BookStoreAPI.Models
{
    public class OrderModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public List<BookModel> Books { get; set; }
    }
}
