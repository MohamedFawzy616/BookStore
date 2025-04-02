using BookStoreTask.Models;

namespace BookStoreTask.DTOs
{
    public class BookReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}