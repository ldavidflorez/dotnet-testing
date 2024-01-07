using System.Text.Json;

namespace MyApp.Namespace
{
    public class PostService : IPostService
    {
        private HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostDto> GetById(int id)
        {
            string url = $"{_httpClient.BaseAddress}/{id}";
            var result = await _httpClient.GetAsync(url);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var post = JsonSerializer.Deserialize<PostDto>(body, options);

            return post;
        }

        public async Task<IEnumerable<PostDto>> GetAll()
        {
            string url = $"https://jsonplaceholder.typicode.com/posts";
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var posts = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return posts;
        }
    }
}