using BookStoreAPI.Data.Entities;

namespace BookStoreAPI.Models
{
    public class OrderModel
    {
        public readonly Guid id;
        public DateTime OrderDate { get; set; }
        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
