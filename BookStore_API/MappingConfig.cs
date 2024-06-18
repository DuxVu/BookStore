using AutoMapper;
using BookStore_API.Models;
using BookStore_API.Models.DTO;

namespace BookStore_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<Book, BookCreateDTO>().ReverseMap();

            CreateMap<Book, BookUpdateDTO>().ReverseMap();

            CreateMap<Author, AuthorDTO>().ReverseMap();

            CreateMap<Author, AuthorCreateDTO>().ReverseMap();

            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();

            CreateMap<Author, Book>().ReverseMap();
        }
    }
}
