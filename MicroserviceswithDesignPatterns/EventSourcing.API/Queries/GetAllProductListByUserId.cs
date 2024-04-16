using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Queries
{
    public class GetAllProductListByUserId : IRequest<List<ProductDto>>
    {
        public int UserId { get; set; }
    }
}
