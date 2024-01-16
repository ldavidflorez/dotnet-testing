using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private StoreContex _context;
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerService _beerService;

        public BeersController(StoreContex context,
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            IBeerService beerService)
        {
            _context = context;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetAll()
        {
            var result = await _beerService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerInsertDto>> Add(BeerInsertDto beerInsertDto)
        {
            var isValid = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }

            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var isValid = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }

            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.BrandID = beerUpdateDto.BrandID;
            beer.Alcohol = beerUpdateDto.Alcohol;

            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
