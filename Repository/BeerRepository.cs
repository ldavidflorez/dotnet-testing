using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class BeerRepository : ICommonRepository<Beer>
    {
        private StoreContex _context;

        public BeerRepository(StoreContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            var result = await _context.Beers.ToListAsync();
            return result;
        }

        public async Task<Beer> GetById(int id)
        {
            var result = await _context.Beers.FindAsync(id);
            return result;
        }

        public async Task Add(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
        }

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer);
            _context.Beers.Entry(beer).State = EntityState.Modified;
        }

        public void Delete(Beer beer)
        {
            _context.Remove(beer);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Beer> Search(Func<Beer, bool> filter) =>
            _context.Beers.Where(filter).ToList();
    }
}