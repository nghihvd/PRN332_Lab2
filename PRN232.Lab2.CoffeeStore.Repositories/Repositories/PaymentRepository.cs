using PRN232.Lab2.CoffeeStore.Repositories.Database;
using PRN232.Lab2.CoffeeStore.Repositories.Interfaces.Repositories;
using PRN232.Lab2.CoffeeStore.Repositories.Models;

namespace PRN232.Lab2.CoffeeStore.Repositories.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
