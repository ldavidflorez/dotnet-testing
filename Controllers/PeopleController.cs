using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people2Service")] IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        public List<Person> GetAll()
        {
            return Repository.People;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetById(int id)
        {
            var person = Repository.People.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound("Person does not exists");
            }

            return Ok(person);
        }

        [HttpGet("Search/{keyWord}")]
        public List<Person> GetByKeyWord(string keyWord)
        {
            return Repository.People.Where(p => p.Name.ToUpper().
                Contains(keyWord.ToUpper())).ToList();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            if (!_peopleService.Validate(person))
            {
                return BadRequest();
            }

            Repository.People.Add(person);

            return NoContent();
        }
    }

    public class Repository
    {
        public static List<Person> People = new List<Person>
        {
            new Person()
            {
                Id = 1,
                Name = "Luis",
                Birthdate = new DateTime(1998, 12, 15)
            },
            new Person()
            {
                Id = 2,
                Name = "Ana",
                Birthdate = new DateTime(1990, 1, 10)
            },
            new Person()
            {
                Id = 3,
                Name = "Raul",
                Birthdate = new DateTime(1975, 8, 13)
            }
        };
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
