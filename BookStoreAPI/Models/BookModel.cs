namespace BookStoreAPI.Models
{
    public class BookModel
    {
        public readonly Guid id;
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
