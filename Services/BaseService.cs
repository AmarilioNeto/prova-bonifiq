using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
namespace ProvaPub.Services
{
    public class BaseService<TEntity> where TEntity : class
    {
        protected TestDbContext _ctx;

        public BaseService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public virtual List<TEntity> ListEntities(int page, int registrosPorPagina)
        {
            int skipCount = (page - 1) * registrosPorPagina;
            return _ctx.Set<TEntity>()                              
                        .Skip(skipCount)
                        .Take(registrosPorPagina)
                        .ToList();
        }


    }
}
