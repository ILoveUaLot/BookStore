using BookStoreAPI.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class OrderModel
    {
        [Required]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Order date is required")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "At least one book is required")]
        [MinLength(1, ErrorMessage = "At least one book is required")]
        public List<BookModel> Books { get; set; }
    }
}
