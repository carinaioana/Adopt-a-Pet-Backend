using AdoptPets.Application.Features.Animals.Commands.CreateAnimal;
using AdoptPets.Application.Features.Animals.Commands.DeleteAnimal.DeleteAnimal;
using AdoptPets.Application.Features.Animals.Commands.UpdateAnimal;
using AdoptPets.Application.Features.Animals.Queries.GetAllByUser;
using AdoptPets.Application.Features.Animals.Queries.GetById;
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteAnimalCommand = new DeleteAnimalCommand() { AnimalId = id };
            await Mediator.Send(deleteAnimalCommand);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateAnimalCommand command)
        {
            if (id != command.AnimalId)
            {
                return BadRequest("The name in the URL does not match the name in the command.");
            }

            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                if (result.Message == "Animal not found")
                {
                    return NotFound(result.Message);
                }

                return BadRequest(result.Message);
            }

            return Ok(result.Message);
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
