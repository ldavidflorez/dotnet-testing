namespace MyApp.Namespace
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Add(BeerInsertDto TI);
        public Task<T> Update(int id, BeerUpdateDto TU);
        public Task<T> Delete(int id);
        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}