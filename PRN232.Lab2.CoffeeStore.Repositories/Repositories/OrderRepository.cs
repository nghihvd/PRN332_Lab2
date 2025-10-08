using PRN232.Lab2.CoffeeStore.Repositories.Database;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.Repositories;
using PRN232.Lab2.CoffeeStore.Repositories.Models;

namespace PRN232.Lab2.CoffeeStore.Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
