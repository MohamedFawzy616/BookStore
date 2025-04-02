using BookStoreTask.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStoreTask.DTOs
{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}