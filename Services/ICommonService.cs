namespace MyApp.Namespace
{
    public interface ICommonService<T, TI, TU>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Add(BeerInsertDto TI);
        public Task<T> Update(int id, BeerUpdateDto TU);
        public Task<T> Delete(int id);
    }
}