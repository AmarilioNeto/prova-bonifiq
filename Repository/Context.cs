using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
  
    public class Context : IDbContext
    {
        private readonly TestDbContext _dbContext;
        public Context(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<Order> Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
