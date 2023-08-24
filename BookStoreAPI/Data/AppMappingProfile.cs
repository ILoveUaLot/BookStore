﻿using AutoMapper;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Models;

namespace BookStoreAPI.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<Order, OrderModel>();
        }
    }
}
