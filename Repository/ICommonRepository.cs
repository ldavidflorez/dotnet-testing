namespace MyApp.Namespace
{
    public interface ICommonRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(int id);
        public Task Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public Task Save();

        public IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
}