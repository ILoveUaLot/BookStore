using AutoMapper;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Models;

namespace BookStoreAPI.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books)).ReverseMap();
        }
    }
}
