using AutoMapper;

namespace MyApp.Namespace
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private ICommonRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }

        public BeerService(ICommonRepository<Beer> beerRepository,
            IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> GetAll()
        {
            var beers = await _beerRepository.GetAll();
            return beers.Select(b => _mapper.Map<BeerDto>(b)).ToList();
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);

            _beerRepository.Update(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            var beerDto = _mapper.Map<BeerDto>(beer);

            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            return beerDto;
        }

        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if (_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count() > 0)
            {
                Errors.Add("The beer name already exists");
                return false;
            }
            return true;
        }

        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name
                && b.BeerID != beerUpdateDto.Id).Count() > 0)
            {
                Errors.Add("The beer name already exists");
                return false;
            }
            return true;
        }
    }
}