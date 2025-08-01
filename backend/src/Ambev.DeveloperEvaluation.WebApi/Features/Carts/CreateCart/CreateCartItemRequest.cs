﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartItemRequest
    {
        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}