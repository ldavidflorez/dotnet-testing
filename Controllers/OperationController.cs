using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Get(decimal a, decimal b)
        {
            return a + b;
        }

        [HttpPost]
        public decimal Add(Numbers numbers, [FromHeader(Name = "Host")] string host,
            [FromHeader(Name = "Content-Type")] string type,
            [FromHeader(Name = "X-Some")] string? some
        )
        {
            Console.WriteLine($"Host: {host}\tType: {type}\tCustom: {some}");
            return numbers.A - numbers.B;
        }

        [HttpPut]
        public decimal Update(decimal a, decimal b)
        {
            return a * b;
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a / b;
        }
    }

    public class Numbers
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }
}
