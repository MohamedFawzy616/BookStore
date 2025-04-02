using System.ComponentModel.DataAnnotations;

namespace BookStoreTask.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Biography { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}