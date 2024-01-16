using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class BeerService : IBeerService
    {
        private StoreContex _context;

        public BeerService(StoreContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDto>> GetAll()
        {
            var result = await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            }).ToListAsync();

            return result;
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return beerDto;
        }

        public async Task<BeerInsertDto> Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };

            return beerDto;
        }
    }
}