using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class SampleService : Sample.SampleBase
    {
        public override async Task<SampleResponse> GetFullName(SampleRequest request, ServerCallContext context)
        {
            var response = new SampleResponse
            {
                FullName = $"{request.FirstName} {request.LastName}"
            };
            return await Task.FromResult(response);
        }
    }
}
