using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class BookModel
    {
        [Required]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

    }
}
