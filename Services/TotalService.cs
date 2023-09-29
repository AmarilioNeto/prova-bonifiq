using ProvaPub.Repository;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class TotalService
    {
        protected TestDbContext _ctx;

        public PaginationService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public virtual List<TEntity> ListEntities(int page )
        {
            int registrosPorPagina = 10;
                .Take(registrosPorPagina)
            return _ctx.Set<TEntity>()
                .Skip((page - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();
        }

    }
}
