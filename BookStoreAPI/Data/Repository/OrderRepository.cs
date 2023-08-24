using BookStoreAPI.Data.Repository.Interfaces;
using BookStoreAPI.Models;

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

        public async Task UpdateAsync(Order entity)
        {
            db.Orders.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}
