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

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
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

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = beerUpdateDto.Name;
            beer.BrandID = beerUpdateDto.BrandID;
            beer.Alcohol = beerUpdateDto.Alcohol;

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

        public async Task<BeerDto> Delete(int id)
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

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return beerDto;
        }
    }
}