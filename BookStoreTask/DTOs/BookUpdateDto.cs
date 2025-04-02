using BookStoreTask.Models;

namespace BookStoreTask.DTOs
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}