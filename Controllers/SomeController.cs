using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Thread.Sleep(1000);
            Console.WriteLine("1. Database connection finished");

            Thread.Sleep(2000);
            Console.WriteLine("2. Sended email");

            Console.WriteLine("All tasks finished");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var task1 = new Task<string>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1. Database connection finished");
                return "1";
            });

            task1.Start();

            var task2 = new Task<string>(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("2. Sended email");
                return "2";
            });

            task2.Start();

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("All tasks finished");

            stopwatch.Stop();

            var response = new Dictionary<string, string>();
            response.Add("task1", result1);
            response.Add("task2", result2);
            response.Add("totalTime", stopwatch.Elapsed.ToString());

            return Ok(response);
        }
    }
}
