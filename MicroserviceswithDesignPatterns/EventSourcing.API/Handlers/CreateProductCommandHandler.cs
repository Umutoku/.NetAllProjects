﻿using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly ProductStream _productStream;

        public CreateProductCommandHandler(ProductStream productStream)
        {
            _productStream = productStream;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _productStream.CreateProduct(request.CreateProductDto);
            await _productStream.SaveAsync();
        }
    }
}
