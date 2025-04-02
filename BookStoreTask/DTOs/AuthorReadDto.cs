namespace BookStoreTask.DTOs
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Biography { get; set; }
        public ICollection<BookReadDto> Books { get; set; }
    }
}