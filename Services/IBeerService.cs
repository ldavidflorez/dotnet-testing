namespace MyApp.Namespace
{
    public interface IBeerService
    {
        public Task<IEnumerable<BeerDto>> GetAll();
        public Task<BeerDto> GetById(int id);
        public Task<BeerDto> Add(BeerInsertDto beerInsertDto);
        public Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto);
        public Task<BeerDto> Delete(int id);
    }
}