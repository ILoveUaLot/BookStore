namespace BookStoreAPI.Data.Entities
{
    public class Order
    {
        public Guid id { get; init; }
        public DateTime OrderDate { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
