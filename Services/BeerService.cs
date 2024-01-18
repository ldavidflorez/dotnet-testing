namespace MyApp.Namespace
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private ICommonRepository<Beer> _beerRepository;

        public BeerService(ICommonRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> GetAll()
        {
            var beers = await _beerRepository.GetAll();
            return beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                BrandID = b.BrandID,
                Alcohol = b.Alcohol
            }).ToList();

        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            Console.WriteLine(beer.BeerID);
            await _beerRepository.Add(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            beer.Name = beerUpdateDto.Name;
            beer.BrandID = beerUpdateDto.BrandID;
            beer.Alcohol = beerUpdateDto.Alcohol;

            _beerRepository.Update(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

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

            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            return beerDto;
        }
    }
}