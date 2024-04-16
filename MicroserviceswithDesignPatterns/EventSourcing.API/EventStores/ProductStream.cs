using EventSourcing.API.DTOs;
using EventSourcing.Shared.Events;
using EventStore.Client;

namespace EventSourcing.API.EventStores
{
    public class ProductStream : AbstractStream
    {
        public static string StreamName => "ProductStream";
        public static string GroupName => "agroup";

        public ProductStream(EventStoreClient eventStoreClient) : base(StreamName, eventStoreClient)
        {
        }

        public void CreateProduct(CreateProductDto createProductDto)
        {
            var productCreatedEvent = new ProductCreatedEvent
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                UserId = createProductDto.UserId
            };

            Events.AddLast(productCreatedEvent);
        }

        public void NameChanged(ChangeProductNameDto changeProductNameDto)
        {
            var nameChangedEvent = new ProductNameChangedEvent
            {
                Id = changeProductNameDto.Id,
                Name = changeProductNameDto.Name
            };

            Events.AddLast(nameChangedEvent);
        }

        public void PriceChanged(ChangeProductPriceDto changeProductPriceDto)
        {
            var priceChangedEvent = new ProductPriceChangedEvent
            {
                Id = changeProductPriceDto.Id,
                Price = changeProductPriceDto.Price
            };

            Events.AddLast(priceChangedEvent);
        }

        public void Deleted(Guid id)
            {
            var deletedEvent = new ProductDeletedEvent
            {
                Id = id
            };

            Events.AddLast(deletedEvent);
        }
        
    }
}
