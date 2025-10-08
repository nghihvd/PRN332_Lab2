
using PRN232.Lab2.CoffeeStore.Repositories.Database;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.Repositories;
using PRN232.Lab2.CoffeeStore.Repositories.Models;

namespace PRN232.Lab2.CoffeeStore.Repositories.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
