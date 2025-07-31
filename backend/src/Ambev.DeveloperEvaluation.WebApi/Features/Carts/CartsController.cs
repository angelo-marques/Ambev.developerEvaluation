using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Responses;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Results;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Results;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{

    [Route("api/carts")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCartAsync([FromBody] CreateCartCommand request, CancellationToken cancellationToken)
        {

            var command = _mapper.Map<CreateCartCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(nameof(GetCartByIdAsync), response);
        }

  
        [HttpGet("{id}", Name = "GetCartByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { Message = "Invalid cart ID." });
            }

            var command = _mapper.Map<GetCartCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
            {
                return NotFound(new { Message = "Cart not found." });
            }

            return Ok(new ApiResponseWithData<GetCartResult>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<GetCartResult>(response)
            });
            
        }

        /// <summary>
        /// Retrieves a paginated list of carts.
        /// </summary>
        /// <param name="pageNumber">Page number (must be 1 or greater).</param>
        /// <param name="pageSize">Number of carts per page (must be between 1 and 100).</param>
        /// <param name="order">Sorting order (optional).</param>
        /// <param name="filter">Filters to apply (optional).</param>
        /// <returns>Paginated list of carts.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartPageAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? order = null)
        {
            var query = new PaginationQuery<ListCartResult>(pageNumber, pageSize, order);

            Application.Pagination.PaginatedResult<ListCartResult> result = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<ListCartResult>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<ListCartResult>(result)
            });
        }

        /// <summary>
        /// Updates an existing cart
        /// </summary>
        /// <param name="id">The unique identifier of the cart to update</param>
        /// <param name="request">The cart update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated cart details</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCartAsync([FromRoute] Guid id, [FromBody] UpdateCartCommand request, CancellationToken cancellationToken)
        {
            // Garante que o ID da rota seja utilizado no comando
            request.Id = id;

            // Envia o comando direto ao Mediator, confiando que os middlewares e validators já garantem a integridade dos dados
            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateCartResult>
            {
                Success = true,
                Message = "Cart update successfully",
                Data = _mapper.Map<UpdateCartResult>(response)
            });
        }

        /// <summary>
        /// Deletes a cart by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the cart to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content if the cart was deleted, or an error response.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCartAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteCartCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
            {
                return NotFound(new { Message = "Cart not found." });
            }
            return Ok(new ApiResponseWithData<DeleteCartResponse>
            {
                Success = true,
                Message = "Cart delete successfully",
                Data = _mapper.Map<DeleteCartResponse>(response)
            });
        }
    }

}
