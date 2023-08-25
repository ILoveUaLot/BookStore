namespace BookStoreAPI.Data.Entities
{
    public class Book
    {
        public Guid id { get; init; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
