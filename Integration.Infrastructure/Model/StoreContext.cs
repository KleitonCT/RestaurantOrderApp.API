using Integration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Integration.Infrastructure.Model
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<LojaDTO> Stores { get; set; }
    }
}
