using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public interface IDbContext
    {
        DbSet<Order> Orders { get; set; }
    }
}
