using AutoMapper;
using BookStoreTask.DTOs;
using BookStoreTask.Models;

namespace BookStoreTask.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Source -> Target
            CreateMap<Book, BookReadDto>();
            CreateMap<BookReadDto, Book>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

            CreateMap<Author, AuthorReadDto>();
            CreateMap<AuthorReadDto, Author>();
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}