using Ambev.DeveloperEvaluation.Application.Carts.GetCart.Responses;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct.Responses;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct.Responses;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct.Responses;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts.Responses;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Commands;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct.Responses;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(nameof(GetProductByIdAsync), _mapper.Map<CreateProductResponse>(response));
        }

        [HttpGet("{id}", Name = nameof(GetProductByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { Message = "Invalid product ID." });
            }

            var command = _mapper.Map<GetProductCommand>(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product successfully",
                Data = _mapper.Map<GetProductResponse>(response)
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsPageAsync([FromQuery] int pageNumber = 1,
                                                              [FromQuery] int pageSize = 10,
                                                              [FromQuery] string? order = null,
                                                              [FromQuery] ListProductsQuery? filter = null)
        {
            var query = new PaginationQuery<ListProductsQuery, ListProductResponse>(pageNumber, pageSize, order, filter);

            Application.Pagination.PaginatedResponse<ListProductResponse> result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            return Ok(new ApiResponseWithData<ListProductResponse>
            {
                Success = true,
                Message = "Product list successfully",
                Data = _mapper.Map<ListProductResponse>(result)
            });
        }
   
        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetProductCategoriesQuery());
            return Ok(categories);
        }
   
        [HttpGet("category/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByCategory(string category,
                                                               [FromQuery] int page = 1,
                                                               [FromQuery] int size = 10,
                                                               [FromQuery] string? order = null)
        {
            var query = new GetProductsByCategoryQuery(category, page, size, order);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;

            var response = await _mediator.Send(request, cancellationToken);

            if (response == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product upadate successfully",
                Data = _mapper.Map<UpdateProductResponse>(response)
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteProductCommand>(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            return Ok(new ApiResponseWithData<DeleteProductResponse>
            {
                Success = true,
                Message = "Product delete successfully",
                Data = _mapper.Map<DeleteProductResponse>(result)
            });
        
        }
    }
}