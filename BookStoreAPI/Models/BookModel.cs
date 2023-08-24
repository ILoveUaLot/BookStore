namespace BookStoreAPI.Models
{
    public class BookModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
