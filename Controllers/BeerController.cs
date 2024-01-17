using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _commonService;

        public BeerController(IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> commonService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetAll()
        {
            var result = await _commonService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _commonService.GetById(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var isValid = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }

            var beerDto = await _commonService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var isValid = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }

            var beerDto = await _commonService.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _commonService.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
