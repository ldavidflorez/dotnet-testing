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

            var task2 = new Task<string>(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("2. Sended email");
                return "2";
            });

            task1.Start();
            task2.Start();

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("All tasks finished");

            stopwatch.Stop();

            return Ok(new
            {
                Task1Result = result1,
                Task2Result = result2,
                Time = stopwatch.Elapsed
            });
        }
    }
}
