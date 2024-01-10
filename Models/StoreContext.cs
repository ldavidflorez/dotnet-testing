using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class StoreContex : DbContext
    {
        public StoreContex(DbContextOptions<StoreContex> options) : base(options)
        {

        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}