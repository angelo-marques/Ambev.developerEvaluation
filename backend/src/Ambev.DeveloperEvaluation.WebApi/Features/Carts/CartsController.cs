using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts.Results;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.Commands;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
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
        public async Task<IActionResult> CreateCartAsync([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
        {
            if( request is null) {
                return BadRequest(new ApiResponseWithData<GetCartResponse>
                {
                    Success = false,
                    Message = "Request is null",
                    Data = null
                });
            }
            
                var validator = new CreateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCartCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            
            return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
            {
                Success = true,
                Message = "Cart created successfully",
                Data = _mapper.Map<CreateCartResponse>(response)
            });
        }

  
        [HttpGet("{id}", Name = "GetCartByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)            
                return BadRequest(new ApiResponseWithData<GetCartResponse>
                {
                    Success = false,
                    Message = "Invalid cart ID or Empty",
                    Data = null
                });

            var request = new GetCartRequest { Id = id };
            var validator = new GetCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetCartCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)            
                return NotFound(new { Message = "Cart not found." });
            
            return Ok(new ApiResponseWithData<GetCartResponse>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<GetCartResponse>(response)
            });            
        }

 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartPageAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? order = null)
        {
            var query = new PaginationQuery<ListCartResult>(pageNumber, pageSize, order);

            PaginatedResult<ListCartResult> result = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<ListCartResult>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<ListCartResult>(result)
            });
        }

      
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCartAsync([FromRoute] Guid id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new ApiResponseWithData<GetCartResponse>
                {
                    Success = false,
                    Message = "Invalid cart ID or Empty",
                    Data = null
                });
            }
            
            if(request.Id != id)
                return BadRequest(new { Message = "Invalid cart ID is not equals" });

            var validator = new UpdateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateCartCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateCartResponse>
            {
                Success = true,
                Message = "Cart updated successfully",
                Data = _mapper.Map<UpdateCartResponse>(response)
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCartAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {

            var request = new DeleteCartRequest { Id = id };
            var validator = new DeleteCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCartCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
            {
                return NotFound(new { Message = "Cart not found." });
            }
            return Ok(new ApiResponseWithData<GetCartResponse>
            {
                Success = true,
                Message = "Cart delete successfully",
                Data = _mapper.Map<GetCartResponse>(response)
            });
        }
    }

}
