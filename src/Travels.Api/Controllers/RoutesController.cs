using Microsoft.AspNetCore.Mvc;
using Travels.Application.UseCases.RouteCases.AddRoute;
using Travels.Application.UseCases.RouteCases.UpdateRoute;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;
using Travels.Application.UseCases.RouteCases.GetRouteById;
using Travels.Application.UseCases.RouteCases.GetRoute;
using Travels.Application.UseCases.RouteCases.DeleteRoute;
using Travels.Application.Interfaces.Repositories;
using Travels.Domain.Entities;

namespace Travels.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly AddRouteUseCase _addRouteUseCase;
        private readonly GetRouteByIdUseCase _getRouteByIdUseCase;
        private readonly GetRouteUseCase _getRouteUseCase;
        private readonly GetCheapestRouteUseCase _getCheapestRouteUseCase;
        private readonly UpdateRouteUseCase _updateRouteUseCase;
        private readonly DeleteRouteUseCase _deleteRouteUseCase;

        public RoutesController(AddRouteUseCase addRouteUseCase,
                                GetRouteByIdUseCase getRouteByIdUseCase,
                                GetRouteUseCase getRouteUseCase,
                                GetCheapestRouteUseCase getCheapestRouteUseCase,
                                UpdateRouteUseCase updateRouteUseCase,
                                DeleteRouteUseCase deleteRouteUseCase)
        {
            _addRouteUseCase = addRouteUseCase;
            _getCheapestRouteUseCase = getCheapestRouteUseCase;
            _updateRouteUseCase = updateRouteUseCase;
            _getRouteByIdUseCase = getRouteByIdUseCase;
            _getRouteUseCase = getRouteUseCase;
            _deleteRouteUseCase = deleteRouteUseCase;
        }

        /// <summary>
        /// Cria uma nova rota de viagem.
        /// </summary>
        /// <param name="dto">Objeto contendo os detalhes da rota.</param>
        /// <returns>Rota criada.</returns>
        /// <response code="201">Retorna a rota criada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AddRouteDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] AddRouteDto dto)
        {
            var result = await _addRouteUseCase.ExecuteAsync(dto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Value?.Id }, result.Value);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Atualiza uma rota de viagem.
        /// </summary>
        /// <param name="dto">Objeto contendo os detalhes da rota.</param>
        /// <returns>Rota criada.</returns>
        /// <response code="200">Retorna a rota alterada com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateRouteDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Route ID mismatch.");
            }

            var result = await _updateRouteUseCase.ExecuteAsync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        /// <summary>
        /// Obtém todas as rotas cadastradas.
        /// </summary>
        /// <returns>Lista de rotas.</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var routes = await _getRouteUseCase.ExecuteAsync();
            if (routes.IsFailure)
            {
                return NotFound(routes.Error);
            }
            return Ok(routes.Value);
        }

        /// <summary>
        /// Obtém rota específica.
        /// </summary>
        /// <returns>Rota específica.</returns>
        /// <response code="200">Retorna a rota.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(long id)
        {
            var result = await _getRouteByIdUseCase.ExecuteAsync(id);
            
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Calcula a rota mais barata entre dois destinos.
        /// </summary>
        /// <param name="from">Local de origem.</param>
        /// <param name="to">Local de destino.</param>
        /// <returns>Rota mais barata.</returns>
        /// <response code="200">Retorna a rota mais barata encontrada.</response>
        /// <response code="404">Se nenhuma rota for encontrada entre os destinos.</response>
        [HttpGet("cheapest/from/{from}/to/{to}")]
        public async Task<IActionResult> GetCheapestRoute(string from, string to)
        {
            var result = await _getCheapestRouteUseCase.ExecuteAsync(from, to);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deleteRouteUseCase.ExecuteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
