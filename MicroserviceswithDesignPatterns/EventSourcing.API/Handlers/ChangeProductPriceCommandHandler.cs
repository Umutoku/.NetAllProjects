﻿using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class ChangeProductPriceCommandHandler : IRequestHandler<ChangeProductPriceCommand>
    {
        private readonly ProductStream _productStream;

        public ChangeProductPriceCommandHandler(ProductStream productStream)
        {
            _productStream = productStream;
        }
        public async Task Handle(ChangeProductPriceCommand request, CancellationToken cancellationToken)
        {
            _productStream.PriceChanged(request.ChangeProductPriceDto);
            await _productStream.SaveAsync();
        }
    }
}
