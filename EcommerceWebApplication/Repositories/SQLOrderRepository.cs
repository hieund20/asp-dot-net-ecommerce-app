using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly EcommerceDBContext dBContext;

        public SQLOrderRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await dBContext.Orders.AddAsync(order);
            await dBContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteAsync(Guid id)
        {
            var existingOrder = await dBContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            if (existingOrder == null)
            {
                return null;
            }

            dBContext.Orders.Remove(existingOrder);
            await dBContext.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<List<Order>> GetAllAsync(string? filterOn = null, 
                                                string? filterQuery = null, 
                                                string? sortBy = null, 
                                                bool isAscending = true, 
                                                int pageNumber = 1, 
                                                int pageSize = 1000)
        {
            var orders = dBContext.Orders.AsQueryable();

            // Filtering
            //if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            //{
            //    if (filterOn.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
            //    {
            //        orders = products.Where(x => x.ProductName.Contains(filterQuery));
            //    }
            //}

            //// Sorting
            //if (string.IsNullOrWhiteSpace(sortBy) == false)
            //{
            //    if (sortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase))
            //    {
            //        products = isAscending ? products.OrderBy(x => x.ProductName) : products.OrderByDescending(x => x.ProductName);
            //    }
            //    else if (sortBy.Equals("UnitPrice", StringComparison.OrdinalIgnoreCase))
            //    {
            //        products = isAscending ? products.OrderBy(x => x.UnitPrice) : products.OrderByDescending(x => x.UnitPrice);
            //    }
            //}

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await orders.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await dBContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<Order?> UpdateAsync(Guid id, Order order)
        {
            var existingOrder = await dBContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.RequiredDate = order.RequiredDate;
            existingOrder.ShippedDate = order.ShippedDate;
            existingOrder.ShipCity = order.ShipCity;
            existingOrder.ShipVia = order.ShipVia;

            await dBContext.SaveChangesAsync();

            return existingOrder;
        }
    }
}
