using AutoMapper;
using BookStoreAPI.Data.Entities;
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
        public async Task<IActionResult> GetOrderByFilter(Guid id, DateTime? orderDate)
        {
            List<Order> orderEntities = await _orderRepo.GetOrdersByFilter(id, orderDate);
            if (orderEntities == null) NotFound();
            List<OrderModel> orderList = _mapper.Map<List<OrderModel>>(orderEntities);
            return Ok(orderList);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderModel orderModel)
        {
            Order order = _mapper.Map<Order>(orderModel);
            if(ModelState.IsValid)
            {
                await _orderRepo.CreateAsync(order);
                return CreatedAtAction(nameof(AddOrder), new {id = order.id});
            }
            return BadRequest();
        }
    }
}
