using BookStoreAPI.Data.Entities;

namespace BookStoreAPI.Data.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByFilter(Guid id, DateTime orderDate);
    }
}
