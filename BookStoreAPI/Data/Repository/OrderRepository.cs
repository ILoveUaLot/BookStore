using BookStoreAPI.Data.Entities;
using BookStoreAPI.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public readonly BookStoreContext db;
        public OrderRepository(BookStoreContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Order entity)
        {
            await db.Orders.AddAsync(entity);    
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            db.Orders.Remove(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return db.Orders;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
           return await db.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrdersByFilter(Guid id, DateTime? orderDate)
        {
            IQueryable<Order> orders = db.Orders.Include(o=>o.Books);
            if(id != Guid.Empty)
            {
                orders = orders.Where(o => o.id == id);
            }
            if(orderDate != null)
            {
                orders = orders.Where(o => o.OrderDate >= orderDate);
            }
            return await orders.ToListAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            db.Orders.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}
