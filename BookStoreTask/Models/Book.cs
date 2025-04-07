using System.ComponentModel.DataAnnotations;

namespace BookStoreTask.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string PublishedDate { get; set; }
        public string ISBN { get; set; }

        //Foreign Keys
        public int AuthorId { get; set; }

        //Navigation Properites
        public Author Author { get; set; }

        public string UserId { get; set; } = string.Empty; // Owner of the book record

        // Navigation property to ApplicationUser if needed
        // public ApplicationUser User { get; set; }

    }
}