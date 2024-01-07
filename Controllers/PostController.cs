using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<PostDto> GetById(int id)
        {
            var result = await _postService.GetById(id);
            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetAll()
        {
            var result = await _postService.GetAll();
            return result;
        }
    }
}
