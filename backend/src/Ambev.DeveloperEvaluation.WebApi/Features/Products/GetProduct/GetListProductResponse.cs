namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetListProductResponse
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<GetProductResponse> Data { get; set; } = new();
    }
}
