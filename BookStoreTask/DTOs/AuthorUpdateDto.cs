using Microsoft.AspNetCore.Routing.Constraints;

namespace BookStoreTask.DTOs
{
    public class AuthorUpdateDto
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Biography { get; set; }
    }
}