using Microsoft.AspNetCore.Mvc;
using Travels.Application.UseCases.PersonCases.AddPerson;
using Travels.Application.UseCases.PersonCases.GetPerson;
using Travels.Application.UseCases.PersonCases.GetPersonById;
using Travels.Application.UseCases.PersonCases.UpdatePerson;
using Travels.Application.UseCases.PersonCases.DeletePerson;
using Travels.Domain.Entities;

namespace Travels.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly AddPersonUseCase _addPersonUseCase;
        private readonly GetPersonUseCase _getPersonUseCase;
        private readonly GetPersonByIdUseCase _getPersonByIdUseCase;
        private readonly UpdatePersonUseCase _updatePersonUseCase;
        private readonly DeletePersonUseCase _deletePersonUseCase;

        public PersonsController(AddPersonUseCase addPersonUseCase,
                                GetPersonUseCase getPersonUseCase,
                                GetPersonByIdUseCase getPersonByIdUseCase,
                                UpdatePersonUseCase updatePersonUseCase,
                                DeletePersonUseCase deletePersonUseCase)
        {
            _addPersonUseCase = addPersonUseCase;
            _getPersonUseCase = getPersonUseCase;
            _getPersonByIdUseCase = getPersonByIdUseCase;
            _updatePersonUseCase = updatePersonUseCase;
            _deletePersonUseCase = deletePersonUseCase;
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="dto">Objeto contendo os detalhes da pessoa.</param>
        /// <returns>Pessoa criada.</returns>
        /// <response code="201">Retorna a pessoa criada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AddPersonDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] AddPersonDto dto)
        {
            var result = await _addPersonUseCase.ExecuteAsync(dto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Value?.Id }, result.Value);
            }
            return BadRequest(result.Error);
        }


        /// <summary>
        /// Obtém todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Lista de pessoas.</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var routes = await _getPersonUseCase.ExecuteAsync();
            if (routes.IsFailure)
            {
                return NotFound(routes.Error);
            }
            return Ok(routes.Value);
        }

        /// <summary>
        /// Obtém pessoa específica.
        /// </summary>
        /// <returns>Pessoa específica.</returns>
        /// <response code="200">Retorna a pessoa.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            var result = await _getPersonByIdUseCase.ExecuteAsync(id);
            
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Atualiza uma Pessoa.
        /// </summary>
        /// <param name="dto">Objeto contendo os detalhes da pessoa.</param>
        /// <returns>Pessoa alterada.</returns>
        /// <response code="200">Retorna a pessoa alterada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdatePersonDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Route ID mismatch.");
            }

            var result = await _updatePersonUseCase.ExecuteAsync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deletePersonUseCase.ExecuteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
