using AutoMapper;
using BookStoreAPI.Data.Repository.Interfaces;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMapper _mapper;
        IOrderRepository _orderRepo;
        public OrderController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepo = orderRepository;
        }

        [HttpGet("{id}&{orderDate}")]
        public async Task<List<OrderModel>> GetOrderByFilter(Guid id, DateTime orderDate)
        {

        }
    }
}
