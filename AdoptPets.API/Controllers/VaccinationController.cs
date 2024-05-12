using AdoptPets.Application.Features.Animals.Queries.GetAll;
using AdoptPets.Application.Features.Animals.Queries.GetById;
using AdoptPets.Application.Features.Vaccinations.Commands.CreateVaccination;
using AdoptPets.Application.Features.Vaccinations.Queries.GetAllVaccinations;
using AdoptPets.Application.Features.Vaccinations.Queries.GetByIdVaccination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{
    public class VaccinationController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateVaccinationCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllVaccinationsQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdVaccinationQuery(id));
            return Ok(result);
        }
    }
}
