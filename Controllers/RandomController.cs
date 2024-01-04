using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomServiceSingleton;
        private IRandomService _randomServiceScope;
        private IRandomService _randomServiceTransient;

        private IRandomService _randomService2Singleton;
        private IRandomService _randomService2Scope;
        private IRandomService _randomService2Transient;

        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomScope")] IRandomService randomServiceScope,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("randomSingleton")] IRandomService randomService2Singleton,
            [FromKeyedServices("randomScope")] IRandomService randomService2Scope,
            [FromKeyedServices("randomTransient")] IRandomService randomService2Transient
            )
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScope = randomServiceScope;
            _randomServiceTransient = randomServiceTransient;
            _randomService2Singleton = randomService2Singleton;
            _randomService2Scope = randomService2Scope;
            _randomService2Transient = randomService2Transient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("Singlenton 1", _randomServiceSingleton.Value);
            result.Add("Scope 1", _randomServiceScope.Value);
            result.Add("Transient 1", _randomServiceTransient.Value);

            result.Add("Singlenton 2", _randomService2Singleton.Value);
            result.Add("Scope 2", _randomService2Scope.Value);
            result.Add("Transient 2", _randomService2Transient.Value);

            return result;
        }
    }
}
