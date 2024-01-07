namespace MyApp.Namespace
{
    public interface IPostService
    {
        public Task<PostDto> GetById(int id);
        public Task<IEnumerable<PostDto>> GetAll();
    }
}