using AdoptPets.Application.Features.Animals.Commands.CreateAnimal;
using AdoptPets.Application.Features.Animals.Commands.UpdateAnimal;
using AdoptPets.Application.Features.Animals.Queries.GetAllByUser;
using AdoptPets.Application.Features.Animals.Queries.GetById;
using AdoptPets.Application.Features.Announcements.Queries.GetAnnouncementsByUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdoptPets.API.Controllers
{
    public class AnimalsController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateAnimalCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateAnimalCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result); 
            }

            return Ok(result);
        }
        [HttpGet("my-animals")]
        public async Task<ActionResult<GetAllAnimalsByUserResponse>> GetMyAnimals()
        {
            var dtos = await Mediator.Send(new GetAllAnimalsByUserQuery());

            if (!dtos.Success)
            {
                return NotFound(dtos);
            }

            return Ok(dtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdAnimalQuery(id));
            return Ok(result);
        }
    }
}
